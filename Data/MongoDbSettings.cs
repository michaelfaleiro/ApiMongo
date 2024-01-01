namespace ApiMongo.Data
{
    public class MongoDbSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public Dictionary<string, string> CollectionsNames { get; set; } = [];
    }
}
