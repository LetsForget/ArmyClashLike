using System;
using UnityEngine;

namespace Common.Inputs.Data
{
    [Serializable]
    public struct KeyCommand
    {
        public KeyCode key;
        public PressType pressType;
    }

    public enum PressType
    {
        Up = 0,
        Down = 1,
        Press = 2
    }
}