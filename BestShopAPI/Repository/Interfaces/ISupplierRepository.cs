
using BestShopAPI.Models;

namespace BestShopAPI.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<List<Supplier>> GetAll();
        public Task<Supplier> GetById(int id);
        public Task<Supplier> Add(Supplier supplier);
        public Task<Supplier> Update(int id, Supplier supplier);
        public Task Delete(int id);
        public Task Clear();
    }
}
