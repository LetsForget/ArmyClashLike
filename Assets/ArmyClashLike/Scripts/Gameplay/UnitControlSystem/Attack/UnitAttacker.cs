using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public class UnitAttacker : IUnitAttacker
    {
        public void TryAttack(ref Unit unit, ref Unit target, float deltaTime)
        {
            if (unit.attackCD <= unit.stats.attackSpeed)
            {
                unit.attackCD += deltaTime;
                return;
            }

            var unitPos = unit.container.Position;
            var targetPos = target.container.Position;

            var attackMinDistance = 0.1 + (unit.container.Size + target.container.Size) / 2;

            if (Vector3.Distance(unitPos, targetPos) > attackMinDistance)
            {
                return;
            }

            unit.attackCD = 0;
            target.stats.health -= unit.stats.attack;
            
            if (target.stats.health <= 0)
            {
                target.isDead = true;
            }
        }
    }
}