using AddressService.Repositories;
using AddressService.Repositories.Interfaces;
using AddressService.Services.Interfaces;

namespace AddressService.IoC
{
    public static class ServiceRegistry

    {

        public static void AddServiceRegistry(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAddressService, Services.AddressService>();

        } 



    }
}
