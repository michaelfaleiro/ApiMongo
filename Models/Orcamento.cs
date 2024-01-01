
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiMongo.Models
{
    public class Orcamento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Cliente { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Carro { get; set; } = null!;
        public string? Placa { get; set; }
        public string? Chassis { get; set; }
        [BsonElement("produtos")]
        [JsonPropertyName("produtos")]
        public List<Produto>? Produtos { get; set; } = [];

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}