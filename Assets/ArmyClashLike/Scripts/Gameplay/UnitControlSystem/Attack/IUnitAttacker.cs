namespace ArmyClashLike.Gameplay
{
    public interface IUnitAttacker
    {
        void TryAttack(ref Unit unit, ref Unit target, float deltaTime);
    }
}