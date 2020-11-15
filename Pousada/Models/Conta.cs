using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pousada.Models
{
    [Table ("Conta")]
    public class Conta
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column (TypeName = "double(18, 2)")]
        public double ValorTotal { get; set; }

        [Required]
        public string FormaPagamento { get; set; }

        [Required]
        public string StatusPagamento { get; set; }

        [Required]
        public int ReservaId { get; set; }

        [ForeignKey ("ReservaId")]
        public Reserva Reserva { get; set; }
    }
}