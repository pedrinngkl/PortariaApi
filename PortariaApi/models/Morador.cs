using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortariaApi.Models
{
    public class Morador
    {
        [Key]
        public int MoradorID { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; } = string.Empty; // <-- CORREÇÃO AQUI

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty; // <-- CORREÇÃO AQUI

        [Required]
        [StringLength(512)]
        public string SenhaHash { get; set; } = string.Empty; // <-- CORREÇÃO AQUI

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Required]
        public int FK_ApartamentoID { get; set; }

        [ForeignKey("FK_ApartamentoID")]
        public Apartamento? Apartamento { get; set; }
    }
}