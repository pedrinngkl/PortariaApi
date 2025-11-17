using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortariaApi.Data;
using PortariaApi.Models;
//using BCrypt.Net;

namespace PortariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoradoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoradoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /api/Moradores
        [HttpPost]
        public async Task<IActionResult> CriarMorador([FromBody] Morador novoMorador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // 2. FAÇA O HASH DA SENHA ANTES DE SALVAR
                // O frontend envia a senha pura (ex: "123456") no campo SenhaHash
                // Nós a substituímos pelo hash seguro.
                //novoMorador.SenhaHash = BCrypt.HashPassword(novoMorador.SenhaHash);

                // 3. O resto do código continua igual
                var apartamento = await _context.Apartamentos.FindAsync(novoMorador.FK_ApartamentoID);
                if (apartamento == null)
                {
                    // Nota: Antes de criar um morador, você precisa ter apartamentos no banco.
                    // Vamos ter que adicionar um Apartamento primeiro para o teste funcionar.
                    return BadRequest("Apartamento não encontrado.");
                }

                _context.Moradores.Add(novoMorador);
                await _context.SaveChangesAsync();

                // Importante: Não retorne a SenhaHash na resposta
                // Vamos criar um "DTO" (Data Transfer Object) para isso mais tarde.
                // Por agora, está ok para testar.

                return CreatedAtAction(nameof(GetMoradorPorId), new { id = novoMorador.MoradorID }, novoMorador);
            }
            catch (DbUpdateException ex)
            {
                // Verifica se o erro é de email duplicado (que definimos como UNIQUE no SQL)
                if (ex.InnerException?.Message.Contains("Duplicate entry") == true)
                {
                    return BadRequest("Este email já está cadastrado.");
                }
                return StatusCode(500, $"Erro interno ao salvar: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // GET: /api/Moradores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Morador>> GetMoradorPorId(int id)
        {
            var morador = await _context.Moradores.FindAsync(id);

            if (morador == null)
            {
                return NotFound();
            }

            return morador;
        }
    }
}