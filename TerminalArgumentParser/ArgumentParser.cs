namespace TerminalArgumentParser
{
    public static class ArgumentParser
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            var result = new Dictionary<string, string>();

            for (var i = 0; i < args.Length; i++)
            {
                var currentArg = args[i];

                if (currentArg.StartsWith("-"))
                {
                    var argumentName = currentArg.Replace("-", string.Empty);

                    if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                    {
                        result[argumentName] = args[i + 1];
                        i++; // Skip next argument since it's used as value
                    }
                    else
                    {
                        result[argumentName] = string.Empty;
                    }
                }
            }

            return result;
        }
    }
}
