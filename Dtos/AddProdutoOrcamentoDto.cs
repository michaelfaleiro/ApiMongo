using System.ComponentModel.DataAnnotations;

namespace ApiMongo.Dtos
{
    public class AddProdutoOrcamentoDto
    {
        [Required(ErrorMessage = "Informe o nome do Produto")]
        public string NomeProduto { get; set; }
        [Required(ErrorMessage = "Informe o Sku do Produto")]

        public string Sku { get; set; }
        public string? Marca { get; set; }
        public double PrecoVenda { get; set; }
        public int Quantidade { get; set; }
        public double PrecoCusto { get; set; }
        public string? Link { get; set; }
        public string? Observacao { get; set; }
    }
}