using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSP_Demo.Application.Models;
using MSP_Demo.Domain;
using MSP_Demo.Domain.ValueObject;

namespace MSP_Demo.Application.Abstraction
{
    public interface IGasStationAppService
    {
        Task<GasStation> AddAsync(GasStationInput obj);
        Task<GasStation> GetByIdAsync(Guid id);
        Task<IEnumerable<GasStation>> GetAllAsync();
        Task<IEnumerable<GasStationLocationValueObject>> GetProximityAsync(double latitude, double longitude, string name);
        Task DeleteAsync(Guid id);
        Task<GasStation> UpdateAsync(Guid Id, GasStationInput obj);
    }
}
