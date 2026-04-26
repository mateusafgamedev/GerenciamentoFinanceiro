using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GerenciamentoFinanceiro.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


    }
}
