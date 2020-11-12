using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pousada.Models.Interfaces;

namespace Pousada.Models
{
    [Table ("RelatorioDiario")]
    public class RelatorioDiario
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column (TypeName = "decimal(18, 2)")]
        public float ValorTotal { get; set; }

        [Required]
        [DataType (DataType.DateTime)]
        public DateTime Data { get; set; }

        [Required]
        public bool Telefonema { get; set; }

        [Required]
        public bool Alimentacao { get; set; }

        [Required]
        [Column (TypeName = "decimal(18, 2)")]
        public float ValorTelefonema { get; set; }

        [Required]
        [Column (TypeName = "decimal(18, 2)")]
        public float ValorAlimentacao { get; set; }

        [ForeignKey ("ContaId")]
        public Conta Conta { get; set; }
    }
}