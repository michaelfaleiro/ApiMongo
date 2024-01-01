using System.ComponentModel.DataAnnotations;

namespace ApiMongo.Dtos
{
    public class AddProdutoOrcamentoDto
    {
        [Required(ErrorMessage = "Informe o nome do Produto")]
        public string NomeProduto { get; set; } = string.Empty;
        [Required(ErrorMessage = "Informe o Sku do Produto")]

        public string Sku { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double Preco { get; set; }
    }
}