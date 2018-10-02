using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MSP_Demo.Domain;
using MSP_Demo.Domain.Config;

namespace MSP_Demo.Repository.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class MSPDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MSPDbContext(IOptions<SettingsConfig> settings)
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);

            CheckIndexContext();
        }

        public IMongoCollection<GasStation> GasStations =>
            _mongoDatabase.GetCollection<GasStation>("GasStations");

        private void CheckIndexContext()
        {
            IMongoCollection<GasStation> collection = _mongoDatabase.GetCollection<GasStation>("GasStations");
            collection.Indexes.CreateOne(new IndexKeysDefinitionBuilder<GasStation>().Geo2DSphere(x => x.Location));
        }
    }
}
