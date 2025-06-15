namespace SOLID_Principles
{
    // 🔹 D - Dependency Inversion Principle

    // "High-level modules should not depend on low-level modules.
    // Both should depend on abstractions."

    // Your core business logic should not depend on concrete implementations
    // like a specific database class. Instead, depend on interfaces or abstractions,
    // and inject the concrete implementation at runtime.

    // Bad
    public class SmtpEmailSender
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"SMTP: Email sent to {to}");
        }
    }

    public class OrderService
    {
        private readonly SmtpEmailSender _emailSender = new SmtpEmailSender(); // tightly coupled

        public void PlaceOrder()
        {
            // Order logic...
            _emailSender.Send("customer@example.com", "Order placed!");
        }
    }

    // Correct
    public interface IEmailSender
    {
        void Send(string to, string message);
    }

    public class SmtpEmailSenderCorrect : IEmailSender
    {
        public void Send(string to, string message)
        {
            Console.WriteLine($"Sending SMTP email to {to}: {message}");
        }
    }

    public class OrderServiceCorrect
    {
        private readonly IEmailSender _emailSender;

        public OrderServiceCorrect(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void PlaceOrder()
        {
            // Order logic...
            _emailSender.Send("customer@example.com", "Order placed!");
        }
    }

}
