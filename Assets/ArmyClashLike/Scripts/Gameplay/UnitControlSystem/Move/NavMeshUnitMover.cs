using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public class NavMeshUnitMover : IUnitMover
    {
        public void Initialize(UnitSet unitSet)
        {
            foreach (var unit in unitSet.units)
            {
                var navMeshAgent = unit.container.NavMeshAgent;
                navMeshAgent.speed = unit.stats.speed;
            }
        }

        public void Move(Unit unit, Vector3 target, float deltaTime)
        {
            unit.container.NavMeshAgent.SetDestination(target);
        }
    }
}