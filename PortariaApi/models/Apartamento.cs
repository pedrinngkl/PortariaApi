using System.ComponentModel.DataAnnotations;

namespace PortariaApi.Models
{
    public class Apartamento
    {
        [Key]
        public int ApartamentoID { get; set; }

        [Required]
        [StringLength(10)]
        public string Numero { get; set; } = string.Empty; // <-- CORREÇÃO AQUI

        [StringLength(10)]
        public string? Bloco { get; set; }

        public ICollection<Morador>? Moradores { get; set; }
    }
}