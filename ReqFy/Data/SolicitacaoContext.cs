using Microsoft.EntityFrameworkCore;
using ReqFy.Models;

namespace ReqFy.Data
{
    public class SolicitacaoContext : DbContext
    {
        public SolicitacaoContext(DbContextOptions<SolicitacaoContext> options) 
        : base (options)
        {
            
        }

        public DbSet<Solicitacao> Solicitacaos { get; set; }
    }
}
