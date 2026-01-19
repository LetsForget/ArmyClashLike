using System;

namespace Common.Inputs.Data
{
    
    public struct AxisResult<T> where T : unmanaged, Enum
    {
        public T axis;
        public float result;
    }
}