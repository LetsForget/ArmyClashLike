using System;
using System.Collections.Generic;
using Common.Inputs.Data;

namespace Common.Inputs
{
    public interface IInputConfig<T> where T : Enum
    {
        public Dictionary<T, KeyCommand> KeyCommands { get; }
        public Dictionary<T, AxisCommand> AxisCommands { get; }
    }
}