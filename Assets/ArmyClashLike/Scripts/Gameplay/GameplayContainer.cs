using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace ArmyClashLike.Gameplay
{
    public class GameplayContainer
    {
        public SceneInstance Scene { get; set; }
        
        public LevelContainer LevelContainer { get; set; }
        public GameObject[] UnitContainers { get; set; }
        
        public UnitSet PlayerUnitSet { get; set; }
        public UnitSet EnemyUnitSet { get; set; }

        public void Clear()
        {
            Scene = default;

            LevelContainer = null;
            UnitContainers = null;
            
            PlayerUnitSet = default;
            EnemyUnitSet = default;
        }
    }
}