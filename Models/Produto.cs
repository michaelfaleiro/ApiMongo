using Microsoft.Extensions.Logging.Abstractions;
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
        public string NomeProduto { get; set; } = null!;
        public string Sku { get; set; } = null!;
        public int Quantidade { get; set; }
        public double Preco { get; set; }

    }
}