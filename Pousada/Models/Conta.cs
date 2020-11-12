using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pousada.Models.Interfaces;

namespace Pousada.Models
{
    [Table ("Conta")]
    public class Conta
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column (TypeName = "decimal(18, 2)")]
        public float ValorTotal { get; set; }

        [Required]
        public string FormaPagamento { get; set; }

        [Required]
        public string Status { get; set; }

        [ForeignKey ("ReservaId")]
        public Reserva Reserva { get; set; }
    }
}