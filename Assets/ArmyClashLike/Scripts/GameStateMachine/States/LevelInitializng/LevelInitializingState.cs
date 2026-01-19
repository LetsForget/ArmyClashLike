using System;
using ArmyClashLike.Gameplay;
using ArmyClashLike.UI;
using ContentLoading;
using Cysharp.Threading.Tasks;
using Logging;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ArmyClashLike.GameStates.States
{
    public class LevelInitializingState : GameState
    {
        private readonly ILevelService levelService;
        private readonly IContentLoader contentLoader;
        private readonly IUnitSpawner unitSpawner;
        private readonly GameplayContainer gContainer;
        private readonly IUIManager<ArmyClashFK> uiManager;
        private readonly ILogsWriter logsWriter;

        private IFormationSelectUI formationSelectOverlay;

        public override GameStateType Type => GameStateType.LevelInitializeState;

        public LevelInitializingState(ILevelService levelService, IContentLoader contentLoader,
            IUnitSpawner unitSpawner, GameplayContainer gContainer, IUIManager<ArmyClashFK> uiManager,
            ILogsWriter logsWriter)
        {
            this.levelService = levelService;
            this.contentLoader = contentLoader;
            this.unitSpawner = unitSpawner;
            this.gContainer = gContainer;
            this.uiManager = uiManager;
            this.logsWriter = logsWriter;
        }

        public override async UniTask OnEnter()
        {
            var curLevel = levelService.CurrentLevel;

            var sceneRef = curLevel.SceneRef;
            var scene = await contentLoader.LoadScene(sceneRef, LoadSceneMode.Additive);
            
            gContainer.Scene = scene;
            SceneManager.SetActiveScene(scene.Scene);

            gContainer.LevelContainer = scene.Scene.GetRootGameObjects()[0].GetComponent<LevelContainer>();
            gContainer.UnitContainers = new GameObject[curLevel.Units.Length];
            for (var i = 0; i < gContainer.UnitContainers.Length; i++)
            {
                gContainer.UnitContainers[i] = await contentLoader.Load<GameObject>(curLevel.Units[i]);
            }

            var unitsCount = levelService.CurrentLevel.EnemyUnitsCount;
            var spawnPos = gContainer.LevelContainer.EnemySpawnPoint.position;
            var enemyPos = gContainer.LevelContainer.PlayerSpawnPoint.position;

            gContainer.EnemyUnitSet = await SpawnSet(unitsCount, spawnPos, enemyPos, curLevel.EnemyFormation, Team.Enemy);
            
            formationSelectOverlay = await uiManager.ShowFrame(FrameType.Overlay, ArmyClashFK.FormationSelect) 
                as IFormationSelectUI;

            if (formationSelectOverlay == null)
            {
                logsWriter.LogError("Failed to load formation select overlay");
                return;
            }

            formationSelectOverlay.LinePressed += FormationSelectOverlayOnLinePressed;
            formationSelectOverlay.SquarePressed += FormationSelectOverlayOnSquarePressed;
            formationSelectOverlay.CirclePressed += FormationSelectOverlayOnCirclePressed;
            formationSelectOverlay.ToFightPressed += OnToFightPressed;
        }

        #region Player spawning

        private void FormationSelectOverlayOnLinePressed() => SpawnPlayerWithFormation(FormationType.Line);
        private void FormationSelectOverlayOnSquarePressed() => SpawnPlayerWithFormation(FormationType.Rectangle);
        private void FormationSelectOverlayOnCirclePressed() => SpawnPlayerWithFormation(FormationType.Circle);

        private async void SpawnPlayerWithFormation(FormationType formationType)
        {
            try
            {
                await UniTask.SwitchToMainThread();

                if (gContainer.PlayerUnitSet.units != null)
                {
                    foreach (var unit in gContainer.PlayerUnitSet.units)
                    {
                        Object.Destroy((unit.container as MonoBehaviour).gameObject);
                    }
                }

                var unitsCount = levelService.CurrentLevel.PlayerUnitsCount;
                var spawnPos = gContainer.LevelContainer.PlayerSpawnPoint.position;
                var enemyPos = gContainer.LevelContainer.EnemySpawnPoint.position;

                gContainer.PlayerUnitSet = await SpawnSet(unitsCount, spawnPos, enemyPos, formationType, Team.Player);

                formationSelectOverlay.ShowToFightButton();
            }
            catch (Exception e)
            {
                throw; // TODO handle exception
            }
        }

        #endregion

        
        private UniTask<UnitSet> SpawnSet(int count, Vector3 ownPos, Vector3 enemyPos, FormationType formationType, Team team)
        {
            var direction = enemyPos - ownPos;
            var spawnInfo = new SpawnInfo(count, ownPos, direction, formationType, team);
            
            return unitSpawner.Spawn(spawnInfo, gContainer.UnitContainers);
        }
        
        private void OnToFightPressed()
        {
            RaiseChangeState(GameStateType.Gameplay);
        }
        
        public override async UniTask OnExit()
        {
            formationSelectOverlay.LinePressed -= FormationSelectOverlayOnLinePressed;
            formationSelectOverlay.SquarePressed -= FormationSelectOverlayOnSquarePressed;
            formationSelectOverlay.CirclePressed -= FormationSelectOverlayOnCirclePressed;
            formationSelectOverlay.ToFightPressed -= OnToFightPressed;
            
            await uiManager.HideFrame(FrameType.Overlay);
        }
    }
}