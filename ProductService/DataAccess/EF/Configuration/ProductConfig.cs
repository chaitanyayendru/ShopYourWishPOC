using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Bson.Serialization;
using ProductService.Domain;

namespace ProductService.DataAccess.EF.Configuration
{
    public class ProductConfig
    {
        public static void Configure()
        {

            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Description).SetIsRequired(true);
            });
        }
    }
}
