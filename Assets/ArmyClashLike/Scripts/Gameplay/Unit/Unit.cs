namespace ArmyClashLike.Gameplay
{
    public struct Unit
    {
        public Team team;
        public Stats stats;
        
        public float attackCD;
        public bool isDead;
        
        public IUnitContainer container;
    }
    
    public enum StatType
    {
        Attack = 10,
        AttackSpeed = 20,
        Health = 30,
        Speed = 40
    }
}