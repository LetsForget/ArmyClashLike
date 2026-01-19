using ArmyClashLike.Gameplay;
using ArmyClashLike.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Logging;
using UI;
using UnityEngine;

namespace ArmyClashLike.GameStates.States
{
    public class GameplayState : GameState
    {
        private readonly IUnitsController unitsController;
        private readonly GameplayContainer gameplayContainer;
        
        private readonly IUIManager<ArmyClashFK> uiManager;
        private readonly ILogsWriter logsWriter;
        
        private IBattleEndUI battleEndUI;
        
        public override GameStateType Type => GameStateType.Gameplay;

        public GameplayState(IUnitsController unitsController, GameplayContainer gameplayContainer, IUIManager<ArmyClashFK> uiManager, ILogsWriter logsWriter)
        {
            this.unitsController = unitsController;
            this.gameplayContainer = gameplayContainer;
            this.uiManager = uiManager;
            this.logsWriter = logsWriter;
        }
        
        public override UniTask OnEnter()
        {
            unitsController.Initialize(gameplayContainer.PlayerUnitSet, gameplayContainer.EnemyUnitSet);
            unitsController.BattleOver += OnBattleOver;

            var camera = gameplayContainer.LevelContainer.MainCamera;
            var gameplayPoint = gameplayContainer.LevelContainer.CameraGameplayPoint;
            
            camera.transform.DOMove(gameplayPoint.position, 0.5f);
            camera.transform.DORotate(gameplayPoint.rotation.eulerAngles, 0.5f);
            
            return UniTask.CompletedTask;
        }
        
        public override void UpdateSelf(float deltaTime)
        {
            unitsController.Update(deltaTime);
        }

        public override void FixedUpdateSelf()
        {
            unitsController.FixedUpdate(Time.fixedDeltaTime);
        }
        
        private async void OnBattleOver(Team winnerTeam)
        {
            battleEndUI = await uiManager.ShowFrame(FrameType.Overlay, ArmyClashFK.BattleEnd) as IBattleEndUI;
            
            if (battleEndUI == null)
            {
                logsWriter.LogError("BattleEndUI is null");
                return;
            }
            
            battleEndUI.BackToMenuPressed += OnBackToMenuPressed;
        }

        private void OnBackToMenuPressed()
        {
            RaiseChangeState(GameStateType.LevelReleaseState);
        }

        public override async UniTask OnExit()
        {
            battleEndUI.BackToMenuPressed -= OnBackToMenuPressed;
            await uiManager.HideFrame(FrameType.Overlay);
        }
    }
}