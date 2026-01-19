using System;

namespace ArmyClashLike.Gameplay
{
    [Serializable]
    public struct SizeModification
    {
        public Modification[] modifications;
        public float sizeChange;
    }
}