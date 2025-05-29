
using Microsoft.AspNetCore.Mvc;
using PackingServiceApi.Models;
using PackingServiceApi.Services;


namespace PackingServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly EmpacotamentoService _packingService;

        public OrdersController(EmpacotamentoService packingService)
        {
            _packingService = packingService;
        }

        [HttpPost("pack")]
        public async Task<IActionResult> Empacotar([FromBody] PedidoRequestDTO request)
        {
            if (request?.Pedidos == null || request.Pedidos.Count == 0)
                return BadRequest("A lista de pedidos não pode ser vazia.");

            var result = _packingService.Empacotar(request);


            return Ok(new { pedidos = result });
        }
    }
}
