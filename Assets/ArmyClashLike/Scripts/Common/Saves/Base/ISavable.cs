using System;

namespace Common.Saves
{
    public interface ISavable<T> where T : Enum
    {
        string SaveKey { get; }
        T SaveCategory { get; }
        string Save();
        void Load(string save);
    }
}