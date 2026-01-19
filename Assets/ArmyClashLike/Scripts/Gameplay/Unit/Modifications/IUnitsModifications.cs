namespace ArmyClashLike.Gameplay
{
    public interface IUnitsModifications
    {
        public Stats BaseStats { get; }
        public ColorModification[] ColorModifications { get; }
        public SizeModification[] SizeModifications { get; }

        ColorModification GetRandomColorModification();
        SizeModification GetRandomSizeModification();
    }
}