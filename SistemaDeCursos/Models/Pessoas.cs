using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeCursos.Models
{
    [Table("pessoas")]
    public class Pessoas
    {
        [Key]
        public int pessoa_id { get; set; }

        [DisplayName("Nome da pessoa")]
        [Required(ErrorMessage = "Informe o campo 'Nome da pessoa'.")]
        [StringLength(256, ErrorMessage = "O campo 'Nome da pessoa' permite no máximo 256 caracteres.")]
        public string pessoa_nome { get; set; }

        [DisplayName("Data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data no formato inválido")]
        public DateTime? data_nascimento { get; set; }

        [NotMapped]
        [DisplayName("Data de nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? data_nascimento_exibicao => data_nascimento;

        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Informe o campo 'Sexo'.")]
        [Range(0, 1, ErrorMessage = "Informe o campo 'Sexo'.")]
        public int sexo { get; set; }

        [NotMapped]
        public string sexo_nome => sexo == 0 ? "Masculino" : "Feminino";

        [DisplayName("Telefone fixo")]
        public string telefone_fixo { get; set; }

        [DisplayName("Telefone móvel")]
        public string telefone_movel { get; set; }

        [DisplayName("Observações")]
        [DataType(DataType.MultilineText)]
        [StringLength(2048, ErrorMessage = "O campo 'Observações' permite no máximo 2048 caracteres.")]
        public string observacoes { get; set; }
    }
}
