using Newtonsoft.Json;
using TerminalArgumentParser;

public class Program
{
    public static void Main(string[] args)
    {
        // Parse fixed command-line arguments into an Arguments object
        var arguments = FixedArgumentParser.Parse<Arguments>(args);

        Console.WriteLine("--------------");
        Console.WriteLine("Arguments object");
        Console.WriteLine();
        Console.WriteLine(arguments == null ? "null" : JsonConvert.SerializeObject(arguments));

        // Parse command-line arguments into key-value list
        var parsedArguments = ArgumentParser.Parse(args);

        Console.WriteLine("--------------");
        Console.WriteLine("Key-value list");
        Console.WriteLine();
        Console.WriteLine(JsonConvert.SerializeObject(parsedArguments));
    }
}