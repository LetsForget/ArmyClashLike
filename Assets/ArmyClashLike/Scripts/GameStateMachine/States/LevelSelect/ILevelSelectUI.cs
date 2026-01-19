using System;

namespace ArmyClashLike.GameStates.States
{
    public interface ILevelSelectUI
    {
        event Action<int> LevelSelect;

        void AddLevel(int index, string label);

        void ClearLevels();
    }
}