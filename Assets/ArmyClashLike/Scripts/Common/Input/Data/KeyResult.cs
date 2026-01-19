using System;

namespace Common.Inputs.Data
{
    public struct KeyResult<T> where T : unmanaged, Enum
    {
        public T commandType;
        public bool value;
    }
}