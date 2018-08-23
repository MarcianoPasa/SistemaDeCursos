using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeCursos.Models
{
    [Table("inscritos")]
    public class Inscritos
    {
        [Key]
        public int inscricao_id { get; set; }

        [DisplayName("Código do curso")]
        public int curso_id { get; set; }

        [NotMapped]
        [DisplayName("Nome do curso")]
        public string curso_nome { get; set; }

        [DisplayName("Código da pessoa")]
        public int pessoa_id { get; set; }

        [NotMapped]
        [DisplayName("Nome da pessoa")]
        public string pessoa_nome { get; set; }
    }
}
