using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Domain
{
    public static class OrderVersions
    {
        public static OrderVersion EffectiveOn(this IEnumerable<OrderVersion> versions, DateTime effectiveDate)
        {
            return versions
                .Where(v => v.IsEffectiveOn(effectiveDate))
                .OrderByDescending(v => v.VersionNumber)
                .FirstOrDefault();
        }
        
        public static OrderVersion WithNumber(this IEnumerable<OrderVersion> versions, int number)
        {
            return versions.First(v => v.VersionNumber == number);
        }
        
        public static OrderVersion FirstVersion(this IEnumerable<OrderVersion> versions)
        {
            return versions.First(v => v.VersionNumber == 1);
        }
        
        public static OrderVersion LastVersion(this IEnumerable<OrderVersion> versions)
        {
            return versions.OrderByDescending(v => v.VersionNumber).First();
        }
    }
}