using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pousada.Models
{
    [Table ("Reserva")]

    public class Reserva
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataType (DataType.DateTime)]
        public DateTime DataEntrada { get; set; }

        [Required]
        [DataType (DataType.DateTime)]
        public DateTime DataSaida { get; set; }

        [Required] 
        public int HospedeId { get; set; }
        
        [Required] 
        public int QuartoId { get; set; }

        [ForeignKey ("HospedeId")]
        public Hospede Hospede { get; set; }

        [ForeignKey ("QuartoId")]
        public Quarto Quarto { get; set; }
    }
}