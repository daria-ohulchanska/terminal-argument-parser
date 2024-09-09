namespace TerminalArgumentParser.Interfaces
{
    public interface IParser<T> where T : new()
    {
        public T Parse(string[] args);
    }
}
