using System;
using ArmyClashLike.GameStates.States;
using UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArmyClashLike.UI
{
    public class MainMenuFrame : Frame<ArmyClashFK>, IMenuUI
    {
        public event Action StartPressed;
        
        [SerializeField] private Button startButton;

        private UnityAction startPressed;

        public override void Initialize()
        {
            startPressed = () => StartPressed?.Invoke();
        }

        public override UniTask Show()
        {
            startButton.onClick.AddListener(startPressed);
            return base.Show();
        }

        public override UniTask Hide()
        {
            startButton.onClick.RemoveAllListeners();
            return base.Hide();
        }
    }
}