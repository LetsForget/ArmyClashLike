using UnityEngine;

namespace ArmyClashLike
{
    [CreateAssetMenu(fileName="LevelsConfig", menuName="ArmyClashLike/Levels Config")]
    public class LevelsConfig : ScriptableObject
    {
        [field: SerializeField] public LevelInfo[] Levels { get; private set; }
    }
}