using GerenciamentoFinanceiro.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace GerenciamentoFinanceiro.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Financeiro> Financas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { CategoriaId = "alimentacao", Nome = "Alimentação" },
                new Categoria { CategoriaId = "salario", Nome = "Salário" },
                new Categoria { CategoriaId = "saude", Nome = "Saúde" },
                new Categoria { CategoriaId = "educacao", Nome = "Educação" },
                new Categoria { CategoriaId = "lazer", Nome = "Lazer" },
                new Categoria { CategoriaId = "imovel", Nome = "Imóvel" }
            );

            modelBuilder.Entity<Transacao>().HasData(
                    new Transacao { TransacaoId = "despesa", Nome = "Despesa" },
                    new Transacao { TransacaoId = "lucro", Nome = "Lucro" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
