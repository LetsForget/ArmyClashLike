using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public interface IUnitMover
    {
        void Initialize(UnitSet unitSet);
        void Move(Unit unit, Vector3 target, float deltaTime);
    }
}