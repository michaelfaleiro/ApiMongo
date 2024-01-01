using System.ComponentModel.DataAnnotations;

namespace ApiMongo.Dtos
{
    public class CreateOrcamentoDto
    {
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Cliente { get; set; } = string.Empty;
        [Required(ErrorMessage = "Telefone é Obrigatório")]
        public string Telefone { get; set; } = string.Empty;
        [Required(ErrorMessage = "Carro é Obrigatório")]
        public string Carro { get; set; } = string.Empty;
        public string? Placa { get; set; } = string.Empty;
        public string? Chassis { get; set; } = string.Empty;
    }
}