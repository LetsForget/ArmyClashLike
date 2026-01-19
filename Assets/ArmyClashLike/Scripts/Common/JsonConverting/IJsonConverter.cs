using Cysharp.Threading.Tasks;

namespace Common.JsonConverting
{
    public interface IJsonConverter
    {
        UniTask<string> Serialize(object obj);
        
        UniTask<T> Deserialize<T>(string json);
    }
}