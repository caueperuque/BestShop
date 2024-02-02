using BestShopAPI.Models;
using BestShopAPI.Repository.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace BestShopAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IBaseRepository baseRepository)
        {
            _connectionString = baseRepository.connectionString;
        }

        public async Task<Product> Add(Product product)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = await sqlConnection.QueryAsync("SELECT * FROM Products WHERE Name = @Name", product);
                var existProduct = query.FirstOrDefault();
                if (existProduct is null)
                {
                    var sInsert = "INSERT INTO Products (ProductId, Name, Description, Price, Quantity, SupplierId) VALUES (@ProductId, @Name, @Description, @Price, @Quantity, @SupplierId)";
                    await sqlConnection.ExecuteScalarAsync(sInsert, product);

                    return product;
                }

                return null!;
            }
        }

        public async Task Clear()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sClear = "DELETE FROM Products";
                await sqlConnection.ExecuteAsync(sClear);
            }
        }

        public async Task Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sDelete = "DELETE FROM Products WHERE ProductId = @Id";

                await sqlConnection.ExecuteAsync(sDelete, new { id });
            }
        }

        public async Task<List<Product>> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var products = await sqlConnection.QueryAsync<Product>("SELECT * FROM Products");
                var suppliers = await sqlConnection.QueryAsync<Supplier>("SELECT * FROM Suppliers");

                foreach (var product in products)
                {
                    foreach (var supplier in suppliers)
                    {
                        if (product.SupplierId == supplier.SupplierId)
                        {
                            product.SupplierName = supplier.Name;
                            break;
                        }
                    }
                }

                return products.ToList();
            }
        }

        public async Task<Product> GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sQuery = "SELECT * FROM Products WHERE ProductId = @Id";
                var product = await sqlConnection.QueryFirstOrDefaultAsync<Product>(sQuery, new { id });
                if (product is null) return null!;

                return new Product {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    SupplierId = product.SupplierId,
                };
            }
        }

        public async Task<Product> Update(int id, Product product)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = await sqlConnection.QueryAsync("SELECT * FROM Products WHERE ProductId = @Id", new { id });
                var existProduct = query.FirstOrDefault();
                var parameters = new
                {
                    id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.Quantity,
                    product.SupplierId,
                };

                var allProducts = await GetAll();

                var existName = allProducts.FirstOrDefault(s => s.Name == parameters.Name);

                if (existName != null)
                {
                    return new Product { Name = "exist" };
                }

                if (existProduct is not null)
                {
                    var sUpdate = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price, Quantity = @Quantity, SupplierId = @SupplierId  WHERE ProductId = @Id";
                    await sqlConnection.ExecuteAsync(sUpdate, parameters);
                    return product;
                }
                return null!;
            }
        }
    }
}
