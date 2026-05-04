using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFinanceiro.Models
{
    public class Financeiro
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Informa uma descrição.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informa o valor.")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "Informa uma data.")]
        public DateTime DataDaOperacao { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria.")]
        public string CategoriaId { get; set; }

        [ValidateNever]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Selecione uma transação.")]
        public string TransacaoId { get; set; }

        [ValidateNever]
        public Transacao Transacao { get; set; }
    }
}
