using System.ComponentModel.DataAnnotations;

namespace PortariaApi.Models
{
    public class Visitante
    {
        [Key]
        public int VisitanteID { get; set; }

        [Required]
        [StringLength(255)]
        public string NomeCompleto { get; set; } = string.Empty; // O 'string.Empty' evita aquele aviso amarelo

        [Required]
        [StringLength(50)]
        public string Documento { get; set; } = string.Empty; // CPF ou RG
    }
}