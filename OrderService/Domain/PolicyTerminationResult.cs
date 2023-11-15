namespace OrderService.Domain
{
    public class OrderTerminationResult
    {
        public OrderVersion TerminalVersion { get; private set; }
        public decimal AmountToReturn { get; private set; }

        public OrderTerminationResult(OrderVersion terminalVersion, decimal amountToReturn)
        {
            TerminalVersion = terminalVersion;
            AmountToReturn = amountToReturn;
        }
    }
}