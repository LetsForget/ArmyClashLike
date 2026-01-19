using System;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace ArmyClashLike.Gameplay.Move
{
    public class ParallelJobSearcher : ITargetSearcher
    {
        private readonly Vector3 deadPosition = new Vector3(0f, -99f, 0f); 
        
        private readonly float searchCooldown;
        
        private NativeArray<int> targetsForPlayerNative;
        private NativeArray<int> targetsForEnemyNative;

        public int[] TargetsForPlayer { get; private set; }
        public int[] TargetsForEnemy { get; private set; }
        
        private DateTime lastSearchTime;
        private bool lastSearchComplete;

        public ParallelJobSearcher(float searchCooldown)
        {
            this.searchCooldown = searchCooldown;
        }
        
        public void Initialize(int playerUnitsCount, int enemyUnitsCount)
        {
            targetsForPlayerNative.Dispose();
            targetsForEnemyNative.Dispose();
            
            targetsForPlayerNative = new NativeArray<int>(playerUnitsCount, Allocator.Persistent);
            targetsForEnemyNative = new NativeArray<int>(enemyUnitsCount, Allocator.Persistent);
            
            TargetsForPlayer = new int[playerUnitsCount];
            TargetsForEnemy = new int[enemyUnitsCount];

            lastSearchComplete = true;
            lastSearchTime = DateTime.Now;
        }
        
        public async UniTask Search(Unit[] playerUnits, Unit[] enemyUnits)
        {
            if (!lastSearchComplete || DateTime.Now - lastSearchTime <= TimeSpan.FromSeconds(searchCooldown))
            {
                return;
            }
            
            lastSearchTime = DateTime.Now;
            lastSearchComplete = false;
            
            var positionsA = new NativeArray<Vector3>(playerUnits.Length, Allocator.TempJob);
            var positionsB = new NativeArray<Vector3>(enemyUnits.Length, Allocator.TempJob);

            for (var i = 0; i < playerUnits.Length; i++)
            {
                positionsA[i] = playerUnits[i].isDead ? deadPosition : playerUnits[i].container.Position;
            }
            
            for (var i = 0; i < enemyUnits.Length; i++)
            {
                positionsB[i] = enemyUnits[i].isDead ? deadPosition : enemyUnits[i].container.Position;
            }

            var jobA = new FindClosestEnemyJob
            {
                deadPosition = deadPosition,
                myPositions = positionsA,
                enemyPositions = positionsB,
                closestEnemies = targetsForPlayerNative
            };
            
            var handleA = jobA.Schedule(positionsA.Length, 64); 
            await handleA.ToUniTask(PlayerLoopTiming.Update);

            for (var i = 0; i < TargetsForPlayer.Length; i++)
            {
                TargetsForPlayer[i] = targetsForPlayerNative[i];
            }
            
            var jobB = new FindClosestEnemyJob
            {
                deadPosition = deadPosition,
                myPositions = positionsB,
                enemyPositions = positionsA,
                closestEnemies = targetsForEnemyNative
            };
            
            var handleB = jobB.Schedule(positionsB.Length, 64);
            await handleB.ToUniTask(PlayerLoopTiming.Update);
            
            for (var i = 0; i < TargetsForEnemy.Length; i++)
            {
                TargetsForEnemy[i] = targetsForEnemyNative[i];
            }
            
            positionsA.Dispose();
            positionsB.Dispose();
            
            lastSearchComplete = true;
        }
    }
}