using System.IO;
using Cysharp.Threading.Tasks;

namespace Common.Storage
{
    public class LocalStorageProvider : IStorageProvider
    {
        public UniTask Write(string save, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            return File.WriteAllTextAsync(path, save).AsUniTask();
        }

        public UniTask<string> Load(string path)
        {
            if (!File.Exists(path))
            {
                return UniTask.FromResult<string>(null);
            }
            
            return File.ReadAllTextAsync(path).AsUniTask();
        }
    }
}