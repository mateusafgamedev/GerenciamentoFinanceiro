namespace GerenciamentoFinanceiro.Models
{
    public class Filtros
    {

        public Filtros(string filtroString)
        {
            FiltroString = filtroString ?? "todos-todos-todos";
        }

        public string FiltroString { get; set; }
        public string CategoriaId { get; set; }
        public string TransacaoId { get; set; }
        public string DataOperacao { get; set; }

    }
}
