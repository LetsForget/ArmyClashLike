using ArmyClashLike.UI;
using Cysharp.Threading.Tasks;
using UI;

namespace ArmyClashLike.GameStates.States
{
    public class InitializeState : GameState
    {
        public override GameStateType Type => GameStateType.Initializing;
        
        private readonly IUIManager<ArmyClashFK> uiManager;
        
        public InitializeState(IUIManager<ArmyClashFK> uiManager)
        {
            this.uiManager = uiManager;
        }

        public override async UniTask OnEnter()
        {
            await uiManager.Initialize();

            RaiseChangeState(GameStateType.Menu);
        }
    }
}