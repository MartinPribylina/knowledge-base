using System.Text;

namespace DesignPatterns.Builder
{
    public class BuilderWrapper
    {
        public class HtmlElement
        {
            public string name, text;
            public List<HtmlElement> elements = new();
            private const int indentSize = 2;

            public HtmlElement(string name, string text)
            {
                this.name = name;
                this.text = text;
            }

            private string ToString(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indentSize * indent);
                sb.AppendLine($"{i}<{name}>");
                if (!string.IsNullOrWhiteSpace(text))
                {
                    sb.Append(new string(' ', indentSize * (indent + 1)));
                    sb.AppendLine(text);
                }
                foreach (var e in elements)
                    sb.Append(e.ToString(indent + 1));
                sb.AppendLine($"{i}</{name}>");
                return sb.ToString();
            }

            public override string ToString()
            {
                return $"{ToString(0)}";
            }
        }

        public class HtmlBuilder
        {
            private HtmlElement root;
            private string rootName;

            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root = new HtmlElement(rootName, "");
            }

            // Returning HtmlBuilder => Fluent Builder
            // Fluent interface = Allows chaining multiple calls
            public HtmlBuilder AddChild(string childName, string childText)
            {
                var child = new HtmlElement(childName, childText);
                root.elements.Add(child);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement(rootName, "");
            }
        }

        public static void Execute()
        {
            Console.WriteLine("Builder");
            Console.WriteLine("-------");
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "Hello").AddChild("li", "World");
            Console.WriteLine(builder);

            builder.Clear();
            builder.AddChild("li", "Goodbye").AddChild("li", "World");
            Console.WriteLine(builder);
            Console.WriteLine();
        }
    }
}
