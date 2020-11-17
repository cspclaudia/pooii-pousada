using Microsoft.EntityFrameworkCore;
using Pousada.Models;

namespace Pousada.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options) : base (options) { }

        public DbSet<Conta> Conta { get; set; }
        public DbSet<Hospede> Hospede { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        public DbSet<RelatorioDiario> RelatorioDiario { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
    }
}