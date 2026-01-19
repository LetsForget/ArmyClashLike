using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public interface IUnitSpawner
    {
        UniTask<UnitSet> Spawn(SpawnInfo spawnInfo, GameObject[] containers);
    }
}