using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    [BurstCompile]
    public struct FindClosestEnemyJob : IJobParallelFor
    {
        [ReadOnly] public Vector3 deadPosition;
        [ReadOnly] public NativeArray<Vector3> myPositions; 
        [ReadOnly] public NativeArray<Vector3> enemyPositions; 
        
        public NativeArray<int> closestEnemies; 

        public void Execute(int index)
        {
            var myPos = myPositions[index];
            var minDistance = float.MaxValue;
            
            var closestIndex = -1;
            
            for (var i = 0; i < enemyPositions.Length; i++)
            {
                if (enemyPositions[i].y - deadPosition.y < 0.1f)
                {
                    continue;
                }
                
                var dist = Vector3.Distance(myPos, enemyPositions[i]);
                
                if (dist >= minDistance)
                {
                    continue;
                }
                
                minDistance = dist;
                closestIndex = i;
            }

            closestEnemies[index] = closestIndex;
        }
    }
}