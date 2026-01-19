namespace ArmyClashLike
{
    public class MultiLevelService : ILevelService
    {
        private readonly LevelsConfig levelsConfig;
        
        public LevelInfo CurrentLevel { get; private set; }
        
        public MultiLevelService(LevelsConfig levelsConfig)
        {
            this.levelsConfig = levelsConfig;
        }

        public LevelInfo[] GetLevels()
        {
            return levelsConfig.Levels;
        }

        public void SetLevel(int level)
        {
            CurrentLevel = levelsConfig.Levels[level];
        }
    }
}