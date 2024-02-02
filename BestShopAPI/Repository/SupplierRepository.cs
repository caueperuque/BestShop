using BestShopAPI.Models;
using BestShopAPI.Repository.Interfaces;
using Dapper;
using System.Data.SqlClient;

namespace BestShopAPI.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string _connectionString;
        public SupplierRepository(IBaseRepository baseRepository)
        {
            _connectionString = baseRepository.connectionString;
        }
        public async Task<Supplier> Add(Supplier supplier)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = await sqlConnection.QueryAsync("SELECT * FROM Suppliers WHERE Name = @Name", supplier);
                var existSupplier = query.FirstOrDefault();
                if (existSupplier is null)
                {
                    var sInsert = "INSERT INTO Suppliers (Name) VALUES (@Name)";
                    await sqlConnection.ExecuteScalarAsync(sInsert, supplier);

                    return supplier;
                }

                return null!;
            }
        }

        public async Task Delete(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sDelete = "DELETE FROM Suppliers WHERE SupplierId = @Id";

                await sqlConnection.ExecuteAsync(sDelete, new { id });
            }
        }

        public async Task<List<Supplier>> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                string sQuery = "SELECT * FROM Suppliers";

                var suppliers = await sqlConnection.QueryAsync<Supplier>(sQuery);
                return suppliers.ToList();
            }
        }

        public async Task<Supplier> GetById(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sQuery = "SELECT * FROM Suppliers WHERE SupplierId = @Id";
                var supplier = await sqlConnection.QueryFirstOrDefaultAsync<Supplier>(sQuery, new { id });
                if (supplier is null) return null!;

                return new Supplier { Name = supplier!.Name, SupplierId = id };
            }
        }

        public async Task<Supplier> Update(int id, Supplier supplier)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = await sqlConnection.QueryAsync("SELECT * FROM Suppliers WHERE SupplierId = @Id", new { id });
                var existSupplier = query.FirstOrDefault();
                var parameters = new
                {
                    id,
                    supplier.Name,
                };

                var allSuppliers = await GetAll();

                var existName = allSuppliers.FirstOrDefault(s => s.Name == parameters.Name);

                if (existName != null)
                {
                    return new Supplier { Name = "exist", SupplierId = id };
                }

                if (existSupplier is not null)
                {
                    var sUpdate = "UPDATE Suppliers SET Name = @Name WHERE SupplierId = @Id";
                    await sqlConnection.ExecuteAsync(sUpdate, parameters);
                    return supplier;
                }
                return null!;
            }
        }

        public async Task Clear()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sClear = "DELETE FROM Suppliers";
                await sqlConnection.ExecuteAsync(sClear);
            }
        }
    }
}
