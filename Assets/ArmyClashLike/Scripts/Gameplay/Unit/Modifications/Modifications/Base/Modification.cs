using System;

namespace ArmyClashLike.Gameplay
{
    [Serializable]
    public struct Modification
    {
        public StatType statType;
        public ModifyType applyType;
        public float applyValue;
    }
}