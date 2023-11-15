using System;
using DashboardService.Domain;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace DashboardService.DataAccess.Elastic
{
    public static class NestInstaller
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection services, string cnString)
        {
            services.AddSingleton(typeof(ElasticClient), svc => CreateElasticClient(cnString));
            services.AddScoped(typeof(IOrderRepository), typeof(ElasticOrderRepository));
            return services;
        }

        private static ElasticClient CreateElasticClient(string cnString)
        {
            var connectionSettings = new ConnectionSettings(new Uri(cnString))
                .DefaultMappingFor<OrderInfo>(m=>
                    m.IndexName("order_stats").IdProperty(d=>d.Number));
            return new ElasticClient(connectionSettings);
        }
    }
}
