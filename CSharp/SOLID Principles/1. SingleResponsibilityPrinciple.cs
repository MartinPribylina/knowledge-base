namespace SOLID_Principles
{
    // 🔹 S - Single Responsibility Principle

    // "A class should have only one reason to change."

    // Each class or module should focus on a single task or responsibility.
    // If a class does more than one thing (e.g. validation, logging, data access),
    // it becomes harder to modify, test, and understand.

    // Bad
    // Handling registration, email sending, and logging — multiple responsibilities in one class.
    public class UserManager
    {
        public void RegisterUser(User user)
        {
            // User registration logic
        }

        public void SendWelcomeEmail(string email)
        {
            // Email sending logic
        }

        public void Log(string message)
        {
            // Logging logic
        }
    }

    // Correct
    public class UserService
    {
        public void RegisterUser(User user)
        {
            // User registration logic
        }

        /*Other user related logic, login, logout,...*/
    }

    public class EmailService
    {
        public void SendWelcomeEmail(string email)
        {
            // Email sending logic
        }

        /*Other email related logic, send password reset,...*/
    }

    public class LoggerService
    {
        public void Log(string message)
        {
            // Logging logic
        }

        /*Other logging related logic, log errors,...*/
    }

    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
