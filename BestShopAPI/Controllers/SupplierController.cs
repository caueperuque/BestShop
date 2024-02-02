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

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var suppliers = await _service.GetAll();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supplier = await _service.GetById(id);
                return Ok(supplier);
            }
            catch (InvalidOperationException ex)
            { 
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Supplier supplier)
        {
            try
            {
                var supplierToInsert = await _service.Add(supplier);
                return Created("Fornecedor adicionado.", supplierToInsert);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Supplier supplier)
        {
            try
            {
                var supplierToUpdated = await _service.Update(id, supplier);
                return Ok(supplierToUpdated);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Clear")]
        public async Task<IActionResult> Clear()
        {
            await _service.Clear();
            return NoContent();
        }
    }
}
