using System.ComponentModel.DataAnnotations;

namespace ApiMongo.Dtos
{
    public record AddProdutoOrcamentoDto
    {
        [Required(ErrorMessage = "Informe o nome do Produto")]
        public string NomeProduto { get; init; } = null!;
        [Required(ErrorMessage = "Informe o Sku do Produto")]

        public string Sku { get; init; } = null!;
        public string? Marca { get; init; }
        public double PrecoVenda { get; init; }
        public int Quantidade { get; init; }
        public string? Link { get; init; }
        public string? Observacao { get; init; }
    }
}