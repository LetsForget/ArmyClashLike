using System;
using Common.Inputs.Data;

namespace Common.Inputs
{
    public interface IInputController<T> where T : unmanaged, Enum
    {
        int AxisCommandsCount { get; }
        int KeyCommandsCount { get; }
        
        void ReadAxis(Span<AxisResult<T>> commands);
        
        void ReadKeys(Span<KeyResult<T>> commands);
    }
}