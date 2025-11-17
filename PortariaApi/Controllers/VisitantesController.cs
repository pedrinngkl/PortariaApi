using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // <-- Importante para o 'FirstOrDefaultAsync'
using PortariaApi.Data;
using PortariaApi.Models;

namespace PortariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VisitantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /api/Visitantes
        [HttpPost]
        public async Task<IActionResult> CriarVisitante([FromBody] Visitante novoVisitante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Lógica inteligente: O visitante já existe (pelo documento)?
                var visitanteExistente = await _context.Visitantes
                    .FirstOrDefaultAsync(v => v.Documento == novoVisitante.Documento);

                if (visitanteExistente != null)
                {
                    // Se ele já existe, não precisa criar de novo.
                    // Apenas retorne o visitante que já estava no banco.
                    return Ok(visitanteExistente);
                }

                // Se não existe, crie um novo
                _context.Visitantes.Add(novoVisitante);
                await _context.SaveChangesAsync();
                
                // Retorna o visitante recém-criado
                return Ok(novoVisitante); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}