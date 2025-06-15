namespace SOLID_Principles
{
    // 🔹 O - Open/Closed Principle

    // "Software entities should be open for extension, but closed for modification."

    // You should be able to add new behavior to a class without changing its existing code.
    // This is commonly achieved using interfaces, abstract classes, and polymorphism.

    // Bad 
    public class ReportGenerator
    {
        public void Generate(string data, string format)
        {
            if (format == "pdf")
            {
                Console.WriteLine($"PDF: {data}");
            }
            else if (format == "html")
            {
                Console.WriteLine($"<html>{data}</html>");
            }
            // Adding new format - It’s not "closed for modification."
            // else if (format == "xml")
            // {
            //     Console.WriteLine($"<xml>{data}</xml>");
            // }
        }
    }

    // Correct
    public interface IReportFormatter
    {
        string Format(string content);
    }

    public class PdfReportFormatter : IReportFormatter
    {
        public string Format(string content) => $"PDF: {content}";
    }

    public class HtmlReportFormatter : IReportFormatter
    {
        public string Format(string content) => $"<html>{content}</html>";
    }

    // Adding new format
    // public class XmlReportFormatter : IReportFormatter
    // {
    //     public string Format(string content) => $"<xml>{content}</xml>";
    // }

    public class ReportGeneratorCorrect
    {
        public void Generate(string data, IReportFormatter formatter)
        {
            var formatted = formatter.Format(data);
            Console.WriteLine(formatted);
        }
    }

}
