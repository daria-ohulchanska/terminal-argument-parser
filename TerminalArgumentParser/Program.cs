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

        // Use the parsed arguments
        for (int i = 0; i < arguments.Repeat; i++)
        {
            string message = $"Hello, {arguments.Name ?? "World"}!";

            if (arguments.Capitalize)
                message.ToUpper();

            if (arguments.Verbose)
            {
                Console.WriteLine($"Iteration {i + 1}: {message}");
            }
            else
            {
                Console.WriteLine(message);
            }
        }


        // Parse command-line arguments into key-value list
        var parsedArguments = ArgumentParser.Parse(args);

        Console.WriteLine("--------------");
        Console.WriteLine("Key-value list");
        Console.WriteLine();

        foreach (var argument in parsedArguments)
        {
            Console.WriteLine($"{argument.Key} - {argument.Value}");
        }
    }
}