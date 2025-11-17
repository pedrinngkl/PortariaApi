using Microsoft.AspNetCore.Mvc;
using PortariaApi.Data;
using PortariaApi.Models;

namespace PortariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiberacoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LiberacoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /api/Liberacoes
        // O app React vai enviar os IDs para cá
        [HttpPost]
        public async Task<IActionResult> CriarLiberacao([FromBody] Liberacao novaLiberacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Define os valores padrão antes de salvar
                novaLiberacao.Status = StatusLiberacao.Pendente; 
                novaLiberacao.DataHoraCriacao = DateTime.Now;

                // TODO: Futuramente, validar se os IDs (Morador, Visitante)
                // realmente existem antes de salvar. Por agora, vamos confiar.

                _context.Liberacoes.Add(novaLiberacao);
                await _context.SaveChangesAsync();

                return Ok(novaLiberacao); // Retorna a liberação criada
            }
            catch (Exception ex)
            {
                // Se um ID (FK) não existir, vai dar um erro de banco aqui
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}