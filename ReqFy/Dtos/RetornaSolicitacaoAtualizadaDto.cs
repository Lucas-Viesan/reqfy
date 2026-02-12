namespace ReqFy.Dtos
{
    public class RetornaSolicitacaoAtualizadaDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset DataAtualizacao { get; set; }
    }
}
