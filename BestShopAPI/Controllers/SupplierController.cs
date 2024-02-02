using BestShopAPI.Models;
using BestShopAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestShopAPISupplier.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;
        private readonly ILogger<SupplierController> _logger;

        public SupplierController(ISupplierService service, ILogger<SupplierController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Método GET de Supplier foi acionado.");
            try
            {
                var suppliers = await _service.GetAll();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Método GET By Id de Supplier foi acionado.");

            try
            {
                var supplier = await _service.GetById(id);
                return Ok(supplier);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Fornecedor com id {id} não foi encontrado.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Supplier supplier)
        {
            _logger.LogInformation("Método POST de Product foi acionado.");

            try
            {
                var supplierToInsert = await _service.Add(supplier);
                _logger.LogInformation($"Produto de Id: {supplierToInsert.SupplierId}, Nome: {supplierToInsert.Name} foi inserido no sistema");
                return Created("Fornecedor adicionado.", supplierToInsert);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Fornecedor já está cadastrado no sistema.");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Método DELETE de Supplier foi acionado.");

            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"Fornecedor com Id: {id} foi deletado do sistema");
                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Supplier supplier)
        {
            _logger.LogInformation("Método PUT de Supplier foi acionado.");

            try
            {
                var supplierToUpdated = await _service.Update(id, supplier);
                _logger.LogInformation($"Fornecedor de Id: {id}, Nome: {supplier.Name} foi atualizado no sistema");
                return Ok(supplierToUpdated);
            }
            catch(ArgumentException ex)
            {
                _logger.LogError("Fornecedor não encontrado no sistema");

                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                _logger.LogError($"Não foi possível atualizar o produto, pois já existe um outro com esse nome: {supplier.Name}");

                return Conflict(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Clear")]
        public async Task<IActionResult> Clear()
        {
            _logger.LogInformation("Todos dados da tabela Suppliers foram deletados");

            await _service.Clear();
            return NoContent();
        }
    }
}
