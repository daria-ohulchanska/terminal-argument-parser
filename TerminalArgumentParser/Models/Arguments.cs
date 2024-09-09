namespace TerminalArgumentParser.Models
{
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
}
