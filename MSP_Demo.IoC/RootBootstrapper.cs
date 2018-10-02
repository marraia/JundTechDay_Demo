using Microsoft.Extensions.DependencyInjection;
using MSP_Demo.Application;
using MSP_Demo.Application.Abstraction;
using MSP_Demo.Repository;
using MSP_Demo.Repository.Abstraction;

namespace MSP_Demo.IoC
{
    public static class RootBootstrapper
    {
        public static void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<IGasStationAppService, GasStationAppService>();
            services.AddScoped<IGasStationRepository, GasStationRepository>();
        }
    }
}
