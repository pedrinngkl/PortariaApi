using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortariaApi.Models
{
    public class Liberacao
    {
        [Key]
        public int LiberacaoID { get; set; }

        [Required]
        public DateTime DataPrevista { get; set; }

        public string? Observacao { get; set; }

        [Required]
        public StatusLiberacao Status { get; set; } = StatusLiberacao.Pendente; // Valor padrão

        public DateTime DataHoraCriacao { get; set; } = DateTime.Now; // Valor padrão

        public DateTime? DataHoraEntrada { get; set; } // '?' permite ser nulo

        // --- Chaves Estrangeiras ---

        [Required]
        public int FK_MoradorID { get; set; }

        [Required]
        public int FK_VisitanteID { get; set; }

        [Required]
        public int FK_ApartamentoID { get; set; }

        // --- Propriedades de Navegação (para o EF Core) ---
        [ForeignKey("FK_MoradorID")]
        public Morador? Morador { get; set; }

        [ForeignKey("FK_VisitanteID")]
        public Visitante? Visitante { get; set; }

        [ForeignKey("FK_ApartamentoID")]
        public Apartamento? Apartamento { get; set; }
    }
}