using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.RestClients
{
    public static class RestClientsInstaller
    {
        public static IServiceCollection AddPricingRestClient(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IPricingClient), typeof(PricingClient));
            services.AddSingleton(typeof(IPricingService), typeof(PricingService));
            return services;
        }
    }
}
