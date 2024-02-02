using BestShopAPI.Models;

namespace BestShopAPI.Services
{
    public interface ISupplierService
    {
        public Task<List<Supplier>> GetAll();
        public Task<Supplier> GetById(int id);
        public Task<Supplier> Add(Supplier supplier);
        public Task<Supplier> Update(int id, Supplier supplier);
        public Task Delete(int id);
        public Task Clear();
    }
}
