using System;
using ArmyClashLike.GameStates.States;
using Cysharp.Threading.Tasks;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ArmyClashLike.UI.FormationSelectTab
{
    public class FormationSelectOverlay : Frame<ArmyClashFK>, IFormationSelectUI
    {
        public event Action ToFightPressed; 
        
        public event Action LinePressed;
        public event Action SquarePressed;
        public event Action CirclePressed;
        
        [SerializeField] private Button lineFormation;
        [SerializeField] private Button squareFormation;
        [SerializeField] private Button circleFormation;

        [SerializeField] private Button toFight;
        
        private UnityAction linePressed, squarePressed, circlePressed, toFightPressed;

        public override void Initialize()
        {
            linePressed = () => LinePressed?.Invoke();
            squarePressed = () => SquarePressed?.Invoke();
            circlePressed = () => CirclePressed?.Invoke();
            toFightPressed = () => ToFightPressed?.Invoke();
        }

        public void ShowToFightButton()
        {
            toFight.gameObject.SetActive(true);
        }
        
        public override UniTask Show()
        {
            toFight.gameObject.SetActive(false);
            
            lineFormation.onClick.AddListener(linePressed);
            squareFormation.onClick.AddListener(squarePressed);
            circleFormation.onClick.AddListener(circlePressed);
            toFight.onClick.AddListener(toFightPressed);
            
            return base.Show();
        }

        public override UniTask Hide()
        {
            lineFormation.onClick.RemoveAllListeners();
            squareFormation.onClick.RemoveAllListeners();
            circleFormation.onClick.RemoveAllListeners();
            toFight.onClick.RemoveAllListeners();
            
            return base.Hide();
        }
    }
}