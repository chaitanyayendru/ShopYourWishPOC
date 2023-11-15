using DashboardService.Api.Queries;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace DashboardService.Test
{
    public static class GetTotalSalesResultAssert
    {
        public static GetTotalSalesResultAssertions Should(this GetTotalSalesResult subject) => new GetTotalSalesResultAssertions(subject);
    }
    
    public class GetTotalSalesResultAssertions : ReferenceTypeAssertions<GetTotalSalesResult,GetTotalSalesResultAssertions>
    {
        protected override string Identifier => "GetTotalSalesResultAssertions";

        public GetTotalSalesResultAssertions(GetTotalSalesResult subject) : base(subject)
        {
        }

        public AndConstraint<GetTotalSalesResultAssertions> HaveTotal(long count, decimal premium)
        {
            Subject.Total.OrdersCount.Should().Be(count);
            Subject.Total.OrderAmount.Should().Be(premium);
            return new AndConstraint<GetTotalSalesResultAssertions>(this);
        }

        public AndConstraint<GetTotalSalesResultAssertions> HaveProductTotal(string product, long count,
            decimal premium)
        {
            var productTotal = Subject.PerProductTotal[product];
            productTotal.Should().NotBeNull();
            productTotal.OrdersCount.Should().Be(count);
            productTotal.OrderAmount.Should().Be(premium);
            return new AndConstraint<GetTotalSalesResultAssertions>(this);
        }
    }
}