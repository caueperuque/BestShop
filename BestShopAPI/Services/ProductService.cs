using BestShopAPI.Models;
using BestShopAPI.Repository.Interfaces;

namespace BestShopAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Product> Add(Product product)
        {
            var productToInsert = await _repository.Add(product);
            if (productToInsert is null)
            {
                throw new InvalidOperationException("Produto já cadastrado");
            }

            return productToInsert;
        }

        public async Task Clear()
        {
            await _repository.Clear();
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _repository.GetAll();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result is null)
            {
                throw new InvalidOperationException("Produto não encontrado.");
            }

            return result;
        }

        public async Task<Product> Update(int id, Product product)
        {
            var result = await _repository.Update(id, product);
            if (result.Name == "exist")
            {
                throw new InvalidOperationException("Produto já está cadastrado.");
            }

            if (result is null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            return result;
        }
    }
}
