using Microsoft.AspNetCore.Mvc;
using CQRSDDDDataRest.Application.Commands;

namespace CQRSDDDDataRest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PagosCommandController : ControllerBase
    {
        private readonly PagarCommandHandler _pagoHandler;

        public PagosCommandController(PagarCommandHandler handler)
        {
            _pagoHandler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPago([FromBody] PagarCommand command)
        {
            var id = await _pagoHandler.HandleAsync(command);
            return CreatedAtAction(nameof(CrearPago), new { id }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var pago = await _pagoHandler.ObtenerPagoPorIdAsync(id);
            if (pago == null)
                return NotFound();      
            if (pago == null)
                return NotFound();
            return Ok(pago);
        }
    }
}
