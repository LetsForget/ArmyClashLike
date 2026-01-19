using UnityEngine;

namespace ArmyClashLike.Gameplay
{
    public class SOUnitsModificationsLibrary : IUnitsModifications
    {
        private readonly UnitsModificationsLibrary library;
        
        public Stats BaseStats => library.BaseStats;
        public ColorModification[] ColorModifications => library.ColorModifications;
        public SizeModification[] SizeModifications => library.SizeModifications;
        
        public SOUnitsModificationsLibrary(UnitsModificationsLibrary library)
        {
            this.library = library;
        }
        
        public ColorModification GetRandomColorModification()
        {
            var randomIndex = Random.Range(0, ColorModifications.Length);
            return ColorModifications[randomIndex];
        }

        public SizeModification GetRandomSizeModification()
        {
            var randomIndex = Random.Range(0, SizeModifications.Length);
            return SizeModifications[randomIndex];
        }
    }
}