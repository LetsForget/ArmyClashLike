using ArmyClashLike.Gameplay;
using ContentLoading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace ArmyClashLike.GameStates.States
{
    public class LevelReleaseState : GameState
    {
        private readonly IContentLoader contentLoader;
        private readonly ILevelService levelService;
        private readonly GameplayContainer gContainer;
        
        public override GameStateType Type => GameStateType.LevelReleaseState;

        public LevelReleaseState(IContentLoader contentLoader, ILevelService levelService, GameplayContainer gContainer)
        {
            this.contentLoader = contentLoader;
            this.levelService = levelService;
            this.gContainer = gContainer;
        }
        
        public override async UniTask OnEnter()
        {
            foreach (var unitRef in levelService.CurrentLevel.Units)
            {
                contentLoader.Release(unitRef);
            }
            
            await contentLoader.ReleaseScene(gContainer.Scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            
            gContainer.Clear();
            
            RaiseChangeState(GameStateType.Menu);
        }
    }
}