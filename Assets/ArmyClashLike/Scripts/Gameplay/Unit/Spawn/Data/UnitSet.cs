namespace ArmyClashLike.Gameplay
{
    public readonly struct UnitSet
    {
        public readonly Unit[] units;

        public UnitSet(Unit[] units)
        {
            this.units = units;
        }
    }
}