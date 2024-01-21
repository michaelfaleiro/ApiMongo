using System.ComponentModel.DataAnnotations;

namespace ApiMongo.Dtos
{
    public record CreateOrcamentoDto
    {
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Cliente { get; init; } = string.Empty;
        [Required(ErrorMessage = "Telefone é Obrigatório")]
        public string Telefone { get; init; } = string.Empty;
        [Required(ErrorMessage = "Carro é Obrigatório")]
        public string Carro { get; init; } = string.Empty;
        public string? Placa { get; init; } = string.Empty;
        public string? Chassis { get; init; } = string.Empty;
    }
}