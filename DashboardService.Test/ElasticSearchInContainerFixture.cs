using DashboardService.DataAccess.Elastic;
using DashboardService.Domain;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.WaitStrategies;
using Nest;
using Xunit;

namespace DashboardService.Test
{
    public class ElasticSearchInContainerFixture 
    {
        private readonly TestcontainersContainer testContainer;
        
        public ElasticSearchInContainerFixture()
        {
            var testContainersBuilder = new TestcontainersBuilder<TestcontainersContainer>()
                .WithImage("elasticsearch:6.4.0")
                .WithName("elasticsearch-33333")
                .WithEnvironment("discovery.type","single-node")
                .WithPortBinding(9200, 9200)
                .WithPortBinding(9300, 9300)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(9200));

            testContainer = testContainersBuilder.Build();
            testContainer.StartAsync().Wait();

            InsertData();
        }
        
        public ElasticClient ElasticClient()
        {
            var connectionSettings = new ConnectionSettings()
                .DefaultMappingFor<OrderInfo>(m=>
                    m.IndexName("order_lab_stats").IdProperty(d=>d.Number));
            return new ElasticClient(connectionSettings);
        }
        private void InsertData()
        {
            var orderRepo = new ElasticOrderRepository(ElasticClient());
            //products Britania,Sunfeast,Tata,ThreeRoses

            var agent = "rohit.solid";
            //january-2020
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-01-01","2020-12-31")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-01-15","2021-01-14")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-01-15","2021-01-14")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Tata")
                    .WithDates("2020-01-15","2021-01-14")
                    .WithPremium(150M)
                    .Build()
            );
            //feb-2020
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-02-01","2021-01-31")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-02-15","2021-02-14")
                    .WithPremium(80M)
                    .Build()
            );
            //march-2020
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-03-01","2021-02-28")
                    .WithPremium(50M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-03-15","2021-03-14")
                    .WithPremium(25M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-03-15","2021-03-14")
                    .WithPremium(15M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-03-15","2021-03-14")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Tata")
                    .WithDates("2020-03-15","2021-03-14")
                    .WithPremium(250M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Tata")
                    .WithDates("2020-03-15","2021-03-14")
                    .WithPremium(250M)
                    .Build()
            );
            
            //agent surya.solid
            agent = "surya.solid";
            //january-2020
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-01-01","2020-12-31")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-01-01","2020-12-31")
                    .WithPremium(50M)
                    .Build()
            );
            //feb-2020
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-02-01","2021-01-31")
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-02-01","2021-01-31")
                    .WithPremium(25M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Tata")
                    .WithDates("2020-02-01","2021-01-31")
                    .WithPremium(50M)
                    .Build()
            );
            //march-2020
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Britania")
                    .WithDates("2020-03-01","2021-02-28")
                    .WithPremium(50M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-03-01","2021-02-28")
                    .WithPremium(50M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Sunfeast")
                    .WithDates("2020-03-01","2021-02-28")
                    .WithPremium(50M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Tata")
                    .WithDates("2020-03-01","2021-02-28")
                    .WithPremium(25M)
                    .Build()
            );
            orderRepo.Save
            (
                OrderDocumentBuilder.Create()
                    .WithAgent(agent)
                    .WithProduct("Tata")
                    .WithDates("2020-03-01","2021-02-28")
                    .WithPremium(25M)
                    .Build()
            );
            
        }
    }

    [CollectionDefinition("ElasticSearch in a container")]
    public class ElasticSearchInContainerCollection : ICollectionFixture<ElasticSearchInContainerFixture>
    {
    }
}