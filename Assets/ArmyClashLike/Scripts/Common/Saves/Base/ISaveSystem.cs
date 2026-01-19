using System;
using Cysharp.Threading.Tasks;

namespace Common.Saves
{
    public interface ISaveSystem<T> where T : Enum
    {
        UniTask LoadSaves();
        
        UniTask ApplySaves();
        
        UniTask Save();
        
        void Add(ISavable<T> savable);
        
        void Remove(ISavable<T> savable);
        
        bool HasSaved(ISavable<T> savable);
        
        bool HasSaved(string savableKey);
    }
}