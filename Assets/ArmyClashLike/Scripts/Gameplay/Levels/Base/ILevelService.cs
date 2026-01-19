namespace ArmyClashLike
{
    public interface ILevelService
    {
        LevelInfo CurrentLevel { get; }
        
        LevelInfo[] GetLevels();
        
        void SetLevel(int level);
    }
}