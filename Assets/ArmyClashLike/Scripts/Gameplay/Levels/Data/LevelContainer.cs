using UnityEngine;

namespace ArmyClashLike
{
    public class LevelContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public Transform EnemySpawnPoint { get; private set; }
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField]  public Transform CameraGameplayPoint { get; private set; }
    }
}