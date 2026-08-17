using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFinanceiro.Models
{
    public class Categoria
    {
        public string CategoriaId { get; set; }

        [Required(ErrorMessage = "Selecione uma transação.")]
        public string Nome { get; set; }
    }
}
