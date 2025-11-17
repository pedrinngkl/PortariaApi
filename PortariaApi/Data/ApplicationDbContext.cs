using Microsoft.EntityFrameworkCore;
using PortariaApi.Models;

namespace PortariaApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração (ex: connection string)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Mapeia as nossas classes para as tabelas do banco
        public DbSet<Apartamento> Apartamentos { get; set; }
        public DbSet<Morador> Moradores { get; set; }

    
        public DbSet<Visitante> Visitantes { get; set; }
        public DbSet<Liberacao> Liberacoes { get; set; }
    }
}