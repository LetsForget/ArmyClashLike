using System;
using Cysharp.Threading.Tasks;
using Logging;
using ArmyClashLike.UI;
using UI;

namespace ArmyClashLike.GameStates.States
{
    public class LevelSelectState : GameState
    {
        private readonly ILevelService levelService;
        private readonly IUIManager<ArmyClashFK> uiManager;
        private readonly ILogsWriter logsWriter;
        
        private readonly Action<int> levelChosenCallback;
        
        public override GameStateType Type => GameStateType.LevelSelect;
        
        private ILevelSelectUI levelSelectFrame;
        
        public LevelSelectState(ILevelService levelService, IUIManager<ArmyClashFK> uiManager, ILogsWriter logsWriter)
        {
            this.levelService = levelService;
            this.uiManager = uiManager;
            this.logsWriter = logsWriter;

            levelChosenCallback = OnLevelChosen;
        }

        public override async UniTask OnEnter()
        {
            levelSelectFrame = await uiManager.ShowFrame(FrameType.Screen, ArmyClashFK.ChooseLevel) as ILevelSelectUI;

            if (levelSelectFrame == null)
            {
                logsWriter.LogError("Failed to load level choose frame");
                return;
            }

            levelSelectFrame.ClearLevels();
            
            var levels = levelService.GetLevels();
            for (var i = 0; i < levels.Length; i++)
            {
                levelSelectFrame.AddLevel(i, levels[i].NameKey);
            }

            levelSelectFrame.LevelSelect += levelChosenCallback;
        }

        private void OnLevelChosen(int level)
        {
            levelService.SetLevel(level);
            RaiseChangeState(GameStateType.LevelInitializeState);
        }

        public override UniTask OnExit()
        {
            levelSelectFrame.LevelSelect -= levelChosenCallback;
            
            uiManager.HideFrame(FrameType.Screen);
            
            return UniTask.CompletedTask;
        }
    }
}