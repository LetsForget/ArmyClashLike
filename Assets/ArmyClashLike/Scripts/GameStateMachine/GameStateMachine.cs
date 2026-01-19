using System.Collections.Generic;
using ArmyClashLike.GameStates.States;
using Common;
using Common.StateMachine;

namespace ArmyClashLike.GameStates
{
    public class GameStateMachine : StateMachine<GameStateType, GameState>
    {
        public GameStateMachine(ICommonFactory factory)
        {
            var initializing = factory.FromNew<InitializeState>();
            var menu = factory.FromNew<MenuState>();
            var levelSelect = factory.FromNew<LevelSelectState>();
            var levelInitialize = factory.FromNew<LevelInitializingState>();
            var gameplay = factory.FromNew<GameplayState>();
            var levelRelease = factory.FromNew<LevelReleaseState>();
            
            states = new Dictionary<GameStateType, GameState>
            {
                { GameStateType.Initializing, initializing },
                { GameStateType.Menu, menu },
                { GameStateType.LevelSelect, levelSelect},
                { GameStateType.LevelInitializeState, levelInitialize },
                { GameStateType.Gameplay, gameplay },
                { GameStateType.LevelReleaseState, levelRelease }
            };
            
            InitializeStates();
        }
    }
}