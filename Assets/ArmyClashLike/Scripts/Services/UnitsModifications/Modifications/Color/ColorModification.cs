using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ArmyClashLike.Gameplay
{
    [Serializable]
    public struct ColorModification
    {
        public Modification[] modifications;
        [FormerlySerializedAs("colorChange")] public Color color;
    }
}