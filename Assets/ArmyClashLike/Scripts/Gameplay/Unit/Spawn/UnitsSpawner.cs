using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ArmyClashLike.Gameplay
{
    public class UnitsSpawner : IUnitSpawner
    {
        private readonly IUnitsModifications modifications;

        public UnitsSpawner(IUnitsModifications modifications)
        {
            this.modifications = modifications;
        }
        
        public async UniTask<UnitSet> Spawn(SpawnInfo spawnInfo, GameObject[] containers)
        {
            var count = spawnInfo.count;
            var spawnPoint = spawnInfo.spawnPoint;
            var formationType = spawnInfo.formationType;
            var direction = spawnInfo.direction;
            var team = spawnInfo.team;
            
            var units = new Unit[count];
            
            var positions = new NativeArray<float3>(count, Allocator.TempJob);
            var job = new FormationJob
            {
                center = new float3(spawnPoint.x, spawnPoint.y, spawnPoint.z),
                count = count,
                type = formationType,
                spacing = 2f,
                positions = positions,
                rotation = quaternion.LookRotation(new float3(direction.x, direction.y, direction.z),
                    new float3(0, 1, 0))
            };

            var handle = job.Schedule();
            await handle.WaitAsync(PlayerLoopTiming.Update);
            
            for (var i = 0; i < count; i++)
            {
                var randomContainer = containers[Random.Range(0, containers.Length)];

                var container = Object.Instantiate(randomContainer, new Vector3(positions[i].x, positions[i].y, positions[i].z),
                    Quaternion.identity).GetComponent<IUnitContainer>();
                
                units[i].container = container;
                
                var sizeModification = modifications.GetRandomSizeModification();
                if (container is ISizeModifiable sizeModifiable)
                {
                    sizeModifiable.Modify(sizeModification);
                }
                
                var colorModification = modifications.GetRandomColorModification();
                if (container is IColorModifiable colorModifiable)
                {
                    colorModifiable.Modify(colorModification);
                }
                
                var stats = modifications.BaseStats;
                stats = ApplyStats(stats, units[i].container.FormModifications);
                stats = ApplyStats(stats, sizeModification.modifications);
                stats = ApplyStats(stats, colorModification.modifications);
                
                units[i].stats = stats;
                units[i].team = team;
            }
            
            positions.Dispose(); 
            
            return new UnitSet(units);
        }

        private Stats ApplyStats(Stats stats, Modification[] modifications)
        {
            foreach (var modification in modifications)
            {
                stats = stats.Apply(modification);
            }
            
            return stats;
        }
    }
}