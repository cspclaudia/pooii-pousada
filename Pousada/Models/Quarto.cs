using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pousada.Models
{
    [Table ("Quarto")]
    public class Quarto
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column (TypeName = "decimal(18, 2)")]
        public float ValorDiaria { get; set; } = 100;

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public bool Disponivel { get; set; }
    }
}