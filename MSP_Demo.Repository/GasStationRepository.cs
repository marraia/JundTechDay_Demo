using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MSP_Demo.Domain;
using MSP_Demo.Domain.Config;
using MSP_Demo.Domain.ValueObject;
using MSP_Demo.Repository.Abstraction;
using MSP_Demo.Repository.Context;

namespace MSP_Demo.Repository
{
    public class GasStationRepository : IGasStationRepository
    {
        private readonly MSPDbContext _context;

        public GasStationRepository(IOptions<SettingsConfig> settings)
        {
            _context = new MSPDbContext(settings);
        }

        public async Task AddAsync(GasStation obj)
        {
            await _context.GasStations
                .InsertOneAsync(obj);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.GasStations
                .DeleteOneAsync(doc => doc.Id == id);
        }

        public async Task<IEnumerable<GasStation>> GetAllAsync()
        {
            return await _context.GasStations
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<GasStation> GetByIdAsync(Guid id)
        {
            return await _context.GasStations
                .AsQueryable()
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GasStationLocationValueObject>> GetProximityAsync(double latitude, double longitude, string name)
        {
            var filter = new BsonDocument { { "name", new BsonRegularExpression($"/^{name}/") } };
            var pipeline = new BsonDocumentPipelineStageDefinition<GasStation, GasStationLocationValueObject>(new BsonDocument
            {
                { "$geoNear", new BsonDocument
                    {
                        { "near", new BsonDocument
                            {
                                { "type", "Point"},
                                { "coordinates", new BsonArray(new[] { latitude, longitude })}
                            }
                        },
                        { "distanceField", "dist.calculated" },
                        { "maxDistance", 10000 },
                        { "query" , filter },
                        { "includeLocs", "dist.location" },
                        { "spherical", true},
                    }
                }
            });

            var colAggregate = _context.GasStations.Aggregate().AppendStage(pipeline);
            return await colAggregate.ToListAsync();
        }

        public async Task UpdateAsync(GasStation obj)
        {
            var filter = Builders<GasStation>.Filter.Eq(x => x.Id, obj.Id);

            var updateDefinition = Builders<GasStation>.Update
                .Set(x => x.Email, obj.Email)
                .Set(x => x.Name, obj.Name)
                .Set(x => x.Location, obj.Location);

            await _context.GasStations.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
