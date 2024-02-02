using BestShopAPI.Models;
using BestShopAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BestShopAPIProduct.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Método GET de Product foi acionado.");
            try
            {
                var products = await _service.GetAll();
                return Ok(products);
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
            _logger.LogInformation("Método GET By Id de Product foi acionado.");
            try
            {
                var product = await _service.GetById(id);
                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Produto com id {id} não foi encontrado.");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            _logger.LogInformation("Método POST de Product foi acionado.");
            try
            {
                var productToInsert = await _service.Add(product);
                _logger.LogInformation($"Produto de Id: {productToInsert.ProductId}, Nome: {productToInsert.Name} foi inserido no sistema");
                return Created("Produto adicionado.", productToInsert);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Produto já está cadastrado no sistema.");
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
            _logger.LogInformation("Método DELETE de Product foi acionado.");
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"Produto com Id: {id} foi deletado do sistema");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            _logger.LogInformation("Método PUT de Product foi acionado.");
            try
            {
                var productToUpdated = await _service.Update(id, product);
                _logger.LogInformation($"Produto de Id: {id}, Nome: {product.Name} foi atualizado no sistema");
                return Ok(productToUpdated);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Produto não encontrado no sistema");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Não foi possível atualizar o produto, pois já existe um outro com esse nome: {product.Name}");
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Algo deu errado: {ex.Message}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Clear")]
        public async Task<IActionResult> Clear()
        {
            _logger.LogInformation("Todos dados da tabela Products foram deletados");
            await _service.Clear();
            return NoContent();
        }
    }
}
