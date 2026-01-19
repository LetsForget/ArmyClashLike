using System;
using System.Collections.Generic;
using Common.Inputs.Data;
using UnityEngine;

namespace Common.Inputs
{
    public abstract class ScriptableObjectInputConfig<T> : ScriptableObject, IInputConfig<T> where T : unmanaged, Enum
    {
        [SerializeField] protected List<T> keys;
        [SerializeField] protected List<KeyCommand> values;

        [SerializeField] protected List<T> axisKeys;
        [SerializeField] protected List<AxisCommand> axisValues;

        public Dictionary<T, KeyCommand> KeyCommands
        {
            get
            {
                if (_keyCommands != null)
                {
                    return _keyCommands;
                }
                
                _keyCommands = new Dictionary<T, KeyCommand>();
                for (var i = 0; i < keys.Count; i++)
                {
                    _keyCommands.Add(keys[i], values[i]);
                }
                
                return _keyCommands;
            }
        }
        private Dictionary<T, KeyCommand> _keyCommands;

        public Dictionary<T, AxisCommand> AxisCommands
        {
            get
            {
                if (_axisCommands != null)
                {
                    return _axisCommands;
                }
                
                _axisCommands = new Dictionary<T, AxisCommand>();
                for (var i = 0; i < axisKeys.Count; i++)
                {
                    _axisCommands.Add(axisKeys[i], axisValues[i]);
                }
                
                return _axisCommands;
            }
        }
        private Dictionary<T, AxisCommand> _axisCommands;
    }
}