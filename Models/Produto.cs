using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiMongo.Models
{



    public class Produto
    {
        public Produto()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        public string Id { get; set; }
        public int Quantidade { get; set; }
        public string Sku { get; set; } = null!;
        public string NomeProduto { get; set; } = null!;
        public string? Marca { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoVenda { get; set; }
        public string? Link { get; set; }
        public string? Observacao { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}