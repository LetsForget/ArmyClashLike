using System;
using ArmyClashLike.GameStates;
using Common;
using Cysharp.Threading.Tasks;
using Logging;
using UnityEngine;
using Zenject;

namespace ArmyClashLike
{
    public class Bootstrapper : MonoBehaviour
    {
        private ILogsWriter logsWriter;
        private ICommonFactory factory;
        
        private GameStateMachine gameStateMachine;

        [Inject]
        private void Construct(ILogsWriter logsWriter, ICommonFactory factory)
        {
            this.logsWriter = logsWriter;
            this.factory = factory;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
            
            gameStateMachine = new GameStateMachine(factory);
            gameStateMachine.ChangeState(GameStateType.Initializing).Forget();
            
            logsWriter.Log("Game was launched");
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            gameStateMachine.UpdateSelf(deltaTime);
        }

        private void FixedUpdate()
        {
            gameStateMachine.FixedUpdateSelf();
        }

        private void OnDrawGizmos()
        {
            #if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
            
            gameStateMachine.OnDrawGizmos();
            #endif
        }
    }
}