using System;

namespace ArmyClashLike.Gameplay
{
    public interface IUnitsController
    {
        event Action<Team> BattleOver;
        
        void Initialize(UnitSet playerUnitSet, UnitSet enemyUnitSet);

        void Update(float deltaTime);
        
        void FixedUpdate(float fixedDeltaTime);
    }
}