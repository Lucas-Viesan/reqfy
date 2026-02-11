using System.ComponentModel.DataAnnotations;

namespace ReqFy.Models
{
    public class Solicitacao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "A descrição não pode exceder 200 caracteres")]
        public string Descricao { get; set; }
        [Required]
        public string Tipo { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataAtualizacao { get; set; }
    }
}
