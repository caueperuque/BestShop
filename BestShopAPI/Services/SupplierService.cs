using BestShopAPI.Models;
using BestShopAPI.Repository.Interfaces;

namespace BestShopAPI.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;
        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<Supplier> Add(Supplier supplier)
        {
            var supplierToInsert = await _repository.Add(supplier);
            if (supplierToInsert is null)
            {
                throw new InvalidOperationException("Fornecedor já cadastrado");
            }

            return supplierToInsert;
        }

        public async Task Clear()
        {
            await _repository.Clear();
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Supplier>> GetAll()
        {
            var suppliers = await _repository.GetAll();
            return suppliers;
        }

        public async Task<Supplier> GetById(int id)
        {
            var result = await _repository.GetById(id);
            if (result is null)
            {
                throw new InvalidOperationException("Fornecedor não encontrado.");
            }

            return result;
        }

        public async Task<Supplier> Update(int id, Supplier supplier)
        {
            var result = await _repository.Update(id, supplier);
            if (result.Name == "exist")
            {
                throw new InvalidOperationException("Nome do fornecedor já está cadastrado.");
            }
            
            if (result is null)
            {
                throw new ArgumentException("Fornecedor não encontrado.");
            }

            return result;
        }
    }
}
