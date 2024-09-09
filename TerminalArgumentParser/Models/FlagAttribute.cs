namespace TerminalArgumentParser.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FlagAttribute : Attribute
    {
        public string Name { get; }

        public FlagAttribute(string name)
        {
            Name = name;
        }
    }
}
