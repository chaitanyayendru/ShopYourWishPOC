using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderService.Api.Commands;
using OrderService.Domain;

namespace OrderService.Commands
{
    public class CreateOfferByAgentHandler : IRequestHandler<CreateOfferByAgentCommand, CreateOfferResult>
    {
        private readonly IUnitOfWork uow;
        private readonly IPricingService pricingService;

        public CreateOfferByAgentHandler(IUnitOfWork uow, IPricingService pricingService)
        {
            this.uow = uow;
            this.pricingService = pricingService;
        }

        public async Task<CreateOfferResult> Handle(CreateOfferByAgentCommand request, CancellationToken cancellationToken)
        {
            //calculate price
            var priceParams = ConstructPriceParams(request);
            var price = await pricingService.CalculatePrice(priceParams);

            
            var o = Offer.ForPriceAndAgent(
                priceParams.ProductCode,
                priceParams.OrderFrom,
                priceParams.OrderTo,
                null,
                price,
                request.AgentLogin
            );

            //create and save offer
            uow.Offers.Add(o);
            await uow.CommitChanges();

            //return result
            return ConstructResult(o);
        }

        private CreateOfferResult ConstructResult(Offer o)
        {
            return new CreateOfferResult
            {
                OfferNumber = o.Number,
                TotalPrice = o.TotalPrice,
                CoversPrices = o.Covers.ToDictionary(c => c.Code, c => c.Price)
            };
        }

        private PricingParams ConstructPriceParams(CreateOfferCommand request)
        {
            return new PricingParams
            {
                ProductCode = request.ProductCode,
                OrderFrom = request.OrderFrom,
                OrderTo = request.OrderTo,
                SelectedCovers = request.SelectedCovers,
                Answers = request.Answers.Select(a => Answer.Create(a.QuestionType, a.QuestionCode, a.GetAnswer())).ToList()
            };
        }
        
    }
}