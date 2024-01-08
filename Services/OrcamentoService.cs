using ApiMongo.Data;
using ApiMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiMongo.Services
{
    public class OrcamentoService
    {
        private readonly IMongoCollection<Orcamento> _orcamentoCollection;

        public OrcamentoService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _orcamentoCollection = database.GetCollection<Orcamento>(mongoDbSettings.Value.CollectionsNames["OrcamentoCollection"]);
        }

        public async Task CreateAsync(Orcamento orcamento)
        {
            await _orcamentoCollection.InsertOneAsync(orcamento);
            return;
        }

        public async Task<Orcamento> UpdateAsync(string id, Orcamento orcamento)
        {
            FilterDefinition<Orcamento> filter = Builders<Orcamento>.Filter.Eq("Id", id);
            UpdateDefinition<Orcamento> update = Builders<Orcamento>.Update
            .Set(x => x.Cliente, orcamento.Cliente)
            .Set(x => x.Telefone, orcamento.Telefone)
            .Set(x => x.Carro, orcamento.Carro)
            .Set(x => x.Placa, orcamento.Placa)
            .Set(x => x.Chassis, orcamento.Chassis);
            var result = await _orcamentoCollection.UpdateOneAsync(filter, update);
            return orcamento;
        }

        public async Task<List<Orcamento>> GetAsync()
        {
            return await _orcamentoCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Orcamento> GetByIdAsync(string id)
        {
            return await _orcamentoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddProdutoOrcamentoAsync(string id, Produto produto)
        {
            FilterDefinition<Orcamento> filter = Builders<Orcamento>.Filter.Eq(orcamento => orcamento.Id, id);

            UpdateDefinition<Orcamento> update = Builders<Orcamento>.Update.AddToSet<Produto>("produtos", produto);
            await _orcamentoCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task AtualizarProdutoOrcamento(string id, string produtoId, Produto produto)
        {
            var filter = Builders<Orcamento>.Filter.Eq(orcamento => orcamento.Id, id)
                & Builders<Orcamento>.Filter.ElemMatch(x => x.Produtos, Builders<Produto>.Filter.Eq(x => x.Id, produtoId));

            var update = Builders<Orcamento>.Update
                .Set("produtos.$.NomeProduto", produto.NomeProduto)
                .Set("produtos.$.Sku", produto.Sku)
                .Set("produtos.$.Quantidade", produto.Quantidade)
                .Set("produtos.$.Marca", produto.Marca)
                .Set("produtos.$.PrecoVenda", produto.PrecoVenda)
                .Set("produtos.$.Link", produto.Link)
                .Set("produtos.$.Observacao", produto.Observacao);

            await _orcamentoCollection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveProdutoAsync(string id, string idProduto)
        {
            FilterDefinition<Orcamento> filter = Builders<Orcamento>.Filter.Eq("Id", id);
            UpdateDefinition<Orcamento> update = Builders<Orcamento>.Update.PullFilter("produtos", Builders<Produto>.Filter.Eq("Id", idProduto));
            await _orcamentoCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Orcamento> filter = Builders<Orcamento>.Filter.Eq("Id", id);
            await _orcamentoCollection.DeleteOneAsync(filter);
            return;
        }
    }
}