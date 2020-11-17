using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pousada.Models
{
    [Table ("RelatorioDiario")]
    public class RelatorioDiario
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column (TypeName = "double(18, 2)")]
        public double ValorTotal { get; set; }

        [Required]
        [DataType (DataType.Date)]
        public DateTime Data { get; set; }

        [Required]
        public bool Telefonema { get; set; }

        [Required]
        public bool Alimentacao { get; set; }

        [Required]
        [Column (TypeName = "double(18, 2)")]
        public double ValorTelefonema { get; set; }

        [Required]
        [Column (TypeName = "double(18, 2)")]
        public double ValorAlimentacao { get; set; }

        [Required] 
        public int ContaId { get; set; }

        [ForeignKey ("ContaId")]
        public Conta Conta { get; set; }
    }
}