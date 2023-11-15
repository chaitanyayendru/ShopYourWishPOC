using System;

namespace OrderService.Domain
{
    public class OrderCover
    {
        public virtual string Code { get; protected set; }
        public virtual decimal Premium { get; protected set; }
        public virtual ValidityPeriod CoverPeriod { get; protected set; }

        public OrderCover(Cover cover, ValidityPeriod coverPeriod)
        {
            Code = cover.Code;
            Premium = cover.Price;
            CoverPeriod = coverPeriod;
        }
        
        protected OrderCover() { } //NH required
        
        public OrderCover EndOn(DateTime endDate)
        {
            var originalDaysCovered = CoverPeriod.Days;
            var daysNotUsed = originalDaysCovered - CoverPeriod.EndOn(endDate).Days;
            var premium = decimal.Round
            (
                this.Premium - (this.Premium * decimal.Divide(daysNotUsed,originalDaysCovered))
                , 2
            );
            
            return new OrderCover
            {
                Code = this.Code,
                Premium = premium,
                CoverPeriod = this.CoverPeriod.EndOn(endDate)
            };
        }


    }
}