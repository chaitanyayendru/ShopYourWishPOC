using OrderService.Domain;

namespace OrderService.Test.Domain
{
    public class OrderHolderFactory
    {
        internal static OrderHolder Abc()
        {
            return new OrderHolder
            (
                "A","B","C", 
                Address.Of("Poland","00-133","Warsaw","Chłodna 52")
            );
        }
    }
}