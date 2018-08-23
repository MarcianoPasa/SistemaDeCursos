using System.Data.Entity;

namespace SistemaDeCursos.Models
{
    public class InscritosContext : DbContext
    {
        public InscritosContext() : base("PgCursos")
        {

        }

        public DbSet<Inscritos> Inscritos { get; set; }
    }
}
