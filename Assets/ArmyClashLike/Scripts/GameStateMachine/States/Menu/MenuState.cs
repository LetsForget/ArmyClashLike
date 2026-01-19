using System;
using ArmyClashLike.UI;
using Cysharp.Threading.Tasks;
using Logging;
using UI;

namespace ArmyClashLike.GameStates.States
{
    public class MenuState : GameState
    {
        private readonly IUIManager<ArmyClashFK> uiManager;
        private readonly ILogsWriter logsWriter;
        
        private IMenuUI menuFrame;

        private readonly Action onStartPressed;

        public override GameStateType Type => GameStateType.Menu;

        public MenuState(IUIManager<ArmyClashFK> uiManager, ILogsWriter logsWriter)
        {
            this.uiManager = uiManager;
            this.logsWriter = logsWriter;

            onStartPressed = OnStartPressed;
        }
        
        public override async UniTask OnEnter()
        {
            menuFrame = await uiManager.ShowFrame(FrameType.Screen, ArmyClashFK.MainMenu) as IMenuUI;

            if (menuFrame == null)
            {
                logsWriter.LogError("Failed to open menu");
                return;
            }
                
            menuFrame.StartPressed += onStartPressed;
        }

        public override UniTask OnExit()
        {
            menuFrame.StartPressed -= onStartPressed;
            uiManager.HideFrame(FrameType.Screen);
            
            return UniTask.CompletedTask;
        }
        
        private void OnStartPressed()
        {
            RaiseChangeState(GameStateType.LevelSelect);
        }
    }
}