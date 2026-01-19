using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Common.JsonConverting
{
    public class UnityJsonConverter : IJsonConverter
    {
        public UniTask<string> Serialize(object obj)
        {
            var json = JsonUtility.ToJson(obj);
            return UniTask.FromResult(json);
        }

        public UniTask<T> Deserialize<T>(string json)
        {
            var result = JsonUtility.FromJson<T>(json);
            return UniTask.FromResult(result);
        }
    }
}