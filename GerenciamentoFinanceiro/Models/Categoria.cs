using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFinanceiro.Models
{
    public class Categoria
    {
        public string CategoriaId { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria.")]
        public string Nome { get; set; }
    }
}
