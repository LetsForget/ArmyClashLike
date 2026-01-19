namespace Common.Parser
{
    public interface IParserByType<T>
    {
        T Parse(string input);
    }
}