using Microsoft.AspNetCore.Mvc;
using PortariaApi.Data;
using PortariaApi.Models;


namespace PortariaApi.Controllers
{
    [Route("api/[controller]")] // Rota: /api/Apartamentos
    [ApiController]
    public class ApartamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApartamentosController(ApplicationDbContext context)
        {
            _context = context; // Recebe o banco de dados
        }

        // POST: /api/Apartamentos
        // Endpoint para criar um novo apartamento
        [HttpPost]
        public async Task<IActionResult> CriarApartamento([FromBody] Apartamento novoApartamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Apartamentos.Add(novoApartamento);
                await _context.SaveChangesAsync(); // Salva no banco

                // Retorna o apartamento criado
                return Ok(novoApartamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}