using System.Data.Entity;

namespace SistemaDeCursos.Models
{
    public class CursosContext : DbContext
    {
        public CursosContext() : base("PgCursos")
        {

        }

        public DbSet<Cursos> Cursos { get; set; }
    }
}
