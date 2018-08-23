using System.Data.Entity;

namespace SistemaDeCursos.Models
{
    public class PessoasContext : DbContext
    {
        public PessoasContext() : base("PgCursos")
        {

        }

        public DbSet<Pessoas> Pessoas { get; set; }
    }
}
