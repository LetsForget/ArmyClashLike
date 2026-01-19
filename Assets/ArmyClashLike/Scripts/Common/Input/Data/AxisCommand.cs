using System;

namespace Common.Inputs.Data
{
    [Serializable]
    public struct AxisCommand
    {
        public string axisName;
        public float sensitivity;
    }
}