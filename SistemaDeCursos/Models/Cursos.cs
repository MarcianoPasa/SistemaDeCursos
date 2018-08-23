using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeCursos.Models
{
    [Table("cursos")]
    public class Cursos
    {
        [Key]
        public int curso_id { get; set; }

        [DisplayName("Nome do curso")]
        [Required(ErrorMessage = "Informe o campo 'Nome do curso'.")]
        [StringLength(256, ErrorMessage = "O campo 'Nome do curso' permite no máximo 256 caracteres.")]
        public string curso_nome { get; set; }

        [DisplayName("Data de início")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data no formato inválido")]
        public DateTime? data_inicio { get; set; }

        [NotMapped]
        [DisplayName("Data de início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_inicio_exibicao => data_inicio;

        [DisplayName("Hora de início")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? hora_inicio { get; set; }

        [DisplayName("Data de término")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data no formato inválido")]
        public DateTime? data_termino { get; set; }

        [NotMapped]
        [DisplayName("Data de término")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_termino_exibicao => data_termino;

        [DisplayName("Hora de término")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? hora_termino { get; set; }

        [DisplayName("Descrição do curso")]
        [DataType(DataType.MultilineText)]
        [StringLength(2048, ErrorMessage = "O campo 'Descrição do curso' permite no máximo 2048 caracteres.")]
        public string descricao { get; set; }
    }
}
