using System;
using ArmyClashLike.Gameplay;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ArmyClashLike
{
    [Serializable]
    public struct LevelInfo 
    {
        [field:SerializeField] public string NameKey { get; private set; }
        [field: SerializeField] public AssetReference SceneRef { get; private set; }
        [field: SerializeField] public AssetReference[] Units { get; private set; }
        
        [field: SerializeField] public int PlayerUnitsCount { get; private set; }
        [field: SerializeField] public int EnemyUnitsCount { get; private set; }
        [field: SerializeField] public FormationType EnemyFormation { get; private set; }
    }
}