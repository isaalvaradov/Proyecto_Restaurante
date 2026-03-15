using Microsoft.AspNetCore.Mvc;
using Proyecto_Restaurante.Models;
using Proyecto_Restaurante.Services;

namespace Proyecto_Restaurante.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatillosApiController : ControllerBase
    {
        private readonly IPlatilloService _service;

        public PlatillosApiController(IPlatilloService service)
        {
            _service = service;
        }

        // GET: api/platillos
        [HttpGet]
        public IActionResult GetPlatillos()
        {
            var platillos = _service.ObtenerTodos();
            return Ok(platillos);
        }

        // GET: api/platillos/1
        [HttpGet("{id}")]
        public IActionResult GetPlatillo(int id)
        {
            var platillo = _service.ObtenerPorId(id);

            if (platillo == null)
                return NotFound();

            return Ok(platillo);
        }

        // POST: api/platillos
        [HttpPost]
        public IActionResult CrearPlatillo([FromBody] Platillo platillo)
        {
            _service.Crear(platillo);
            return Ok(platillo);
        }

        // PUT: api/platillos/1
        [HttpPut("{id}")]
        public IActionResult ActualizarPlatillo(int id, [FromBody] Platillo platillo)
        {
            if (id != platillo.PlatilloId)
                return BadRequest();

            _service.Editar(platillo);
            return Ok(platillo);
        }

        // DELETE: api/platillos/1
        [HttpDelete("{id}")]
        public IActionResult EliminarPlatillo(int id)
        {
            _service.Eliminar(id);
            return Ok();
        }
    }
}