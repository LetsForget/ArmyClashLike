using System;
using ArmyClashLike.GameStates.States;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArmyClashLike.UI.EndBattle
{
    public class BattleEndOverlay : Frame<ArmyClashFK>, IBattleEndUI
    {
        public event Action BackToMenuPressed;

        [SerializeField] private Button backToMenuButton;

        private UnityAction backToMenuPressed;

        public override void Initialize()
        {
            backToMenuPressed = () => BackToMenuPressed?.Invoke();
        }

        public override UniTask Show()
        {
            backToMenuButton.onClick.AddListener(backToMenuPressed);
            return base.Show();
        }

        public override UniTask Hide()
        {
            backToMenuButton.onClick.RemoveAllListeners();
            return base.Hide();
        }
    }
}