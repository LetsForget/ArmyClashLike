using System;
using Common.Inputs.Data;
using UnityEngine;

namespace Common.Inputs
{
    public class StandaloneInputController<T> : IInputController<T> where T : unmanaged, Enum
    {
        private readonly IInputConfig<T> config;

        public int AxisCommandsCount { get; private set; }
        public int KeyCommandsCount { get; private set; }
        
        public StandaloneInputController(IInputConfig<T> config)
        {
            this.config = config;
            
            AxisCommandsCount = config.AxisCommands.Count;
            KeyCommandsCount = config.KeyCommands.Count;
        }
        
        public void ReadAxis(Span<AxisResult<T>> commands)
        {
            var index = 0;

            foreach (var axisCommand in config.AxisCommands)
            {
                var axisName = axisCommand.Value.axisName;
                var sensitivity = axisCommand.Value.sensitivity;
                
                commands[index].axis = axisCommand.Key;
                commands[index].result = Input.GetAxis(axisName) * sensitivity;
                
                index++;
            }
        }

        public void ReadKeys(Span<KeyResult<T>> commands)
        {
            var index = 0;

            foreach (var keyCommand in config.KeyCommands)
            {
                var keyCode = keyCommand.Value.key;
                var pressType = keyCommand.Value.pressType;
                
                commands[index].commandType = keyCommand.Key;

                commands[index].value = pressType switch
                {
                    PressType.Up => Input.GetKeyUp(keyCode),
                    PressType.Down => Input.GetKeyDown(keyCode),
                    PressType.Press => Input.GetKey(keyCode),
                };

                index++;
            }
        }
    }
}