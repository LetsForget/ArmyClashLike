using UnityEngine;
using UnityEngine.AI;

namespace ArmyClashLike.Gameplay
{
    public interface IUnitContainer
    {
        Modification[] FormModifications { get; }
        Vector3 Position { get; }
        Rigidbody Rigidbody { get; }
        NavMeshAgent NavMeshAgent { get; }
        float Size { get; }
        
        void PlayDead();
    }
}