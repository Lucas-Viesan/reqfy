using System.ComponentModel.DataAnnotations;

namespace ReqFy.Dtos
{
    public class CriarSolicitacaoDto
    {
        [Required]
        [StringLength(200, ErrorMessage = "A descrição não pode exceder 200 caracteres")]
        public string Descricao { get; set; }
        [Required]
        public string Tipo { get; set; }
    }
}
