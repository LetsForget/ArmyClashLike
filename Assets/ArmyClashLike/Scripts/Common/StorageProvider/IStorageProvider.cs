using Cysharp.Threading.Tasks;

namespace Common.Storage
{
    public interface IStorageProvider
    {
        public UniTask Write(string save, string path);

        public UniTask<string> Load(string path);
    }
}