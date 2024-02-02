using BestShopAPI.Models;

namespace BestShopAPI.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> GetById(int id);
        public Task<Product> Add(Product product);
        public Task<Product> Update(int id, Product product);
        public Task Delete(int id);
        public Task Clear();
    }
}
