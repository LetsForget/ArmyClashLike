using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    [CreateAssetMenu(fileName = "ModificationsLibrary", menuName = "ArmyClashLike/ModificationsLibrary")]
    public class UnitsModificationsLibrary : ScriptableObject
    {
        [field: SerializeField] public Stats BaseStats { get; private set; }
        [field: SerializeField] public ColorModification[] ColorModifications { get; private set; }
        [field: SerializeField] public SizeModification[] SizeModifications { get; private set; }
    }
}