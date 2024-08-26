using System.Reflection;

namespace TerminalArgumentParser
{
    public static class FixedArgumentParser
    {
        public static T Parse<T>(string[] args) where T : new()
        {
            T result = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int i = 0; i < args.Length; i++)
            {
                var currentArg = args[i];

                // Handle Options (those that expect a value)
                var optionProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<OptionAttribute>()?.Name == currentArg);
                if (optionProperty != null)
                {
                    var optionAttribute = optionProperty.GetCustomAttribute<OptionAttribute>();
                    if (i + 1 >= args.Length || args[i + 1].StartsWith("--"))
                    {
                        Console.WriteLine($"Error: {optionAttribute?.Name} requires a value.");
                        return default;
                    }
                    string value = args[++i];

                    // Convert the value to the correct type and assign it to the property
                    var convertedValue = Convert.ChangeType(value, optionProperty.PropertyType);
                    optionProperty.SetValue(result, convertedValue);
                    continue;
                }

                // Handle Flags (those that do not expect a value)
                var flagProperty = properties.FirstOrDefault(p => p.GetCustomAttribute<FlagAttribute>()?.Name == currentArg);
                if (flagProperty != null)
                {
                    flagProperty.SetValue(result, true);
                    continue;
                }

                Console.WriteLine($"Unknown argument: {currentArg}");
                return default;
            }

            return result;
        }
    }

    public class Arguments
    {
        [Option("--name")]
        public string Name { get; set; }

        [Option("--repeat")]
        public int Repeat { get; set; } = 1;

        [Flag("--verbose")]
        public bool Verbose { get; set; }

        [Flag("--capitalize")]
        public bool Capitalize { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FlagAttribute : Attribute
    {
        public string Name { get; }

        public FlagAttribute(string name)
        {
            Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionAttribute : Attribute
    {
        public string Name { get; }

        public OptionAttribute(string name)
        {
            Name = name;
        }
    }
}
