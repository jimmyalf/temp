namespace Synologen.OrderEmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Initialize();

            var orderSender = new OrderSender();
            orderSender.SendOrders();
        }
    }
}
