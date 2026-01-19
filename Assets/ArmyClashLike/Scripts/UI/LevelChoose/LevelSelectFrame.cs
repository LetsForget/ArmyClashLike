using System;
using System.Collections.Generic;
using ArmyClashLike.GameStates.States;
using Common.Pool;
using UI;
using UnityEngine;

namespace ArmyClashLike.UI
{
    public class LevelSelectFrame : Frame<ArmyClashFK>, ILevelSelectUI
    {
        public event Action<int> LevelSelect;
        
        [SerializeField] private LevelSelectButton buttonOriginal;
        [SerializeField] private RectTransform buttonsHolder;
        [SerializeField] private int buttonsPoolSize;
        
        private Pool<LevelSelectButton> buttons;
        private List<LevelSelectButton> activeButtons;
        
        private Action<int> levelSelectCallback;
        
        public override void Initialize()
        {
            levelSelectCallback = OnButtonPressed;
            
            activeButtons = new List<LevelSelectButton>(buttonsPoolSize);
            buttons = new Pool<LevelSelectButton>(CreateButtons());
            IEnumerable<LevelSelectButton> CreateButtons()
            {
                for (var i = 0; i < buttonsPoolSize; i++)
                {
                    var button =  Instantiate(buttonOriginal, buttonsHolder);
                    button.ButtonPressed += levelSelectCallback;
                    
                    yield return button;
                }
            }
        }

        public void AddLevel(int index, string label)
        {
            var button = buttons.GetObject();
            button.Set(index, label);
            
            activeButtons.Add(button);
        }

        public void ClearLevels()
        {
            foreach (var button in activeButtons)
            {
                buttons.TakeObject(button);
            }
            
            activeButtons.Clear();
        }
        
        private void OnButtonPressed(int level)
        {
            LevelSelect?.Invoke(level);
        }
    }
}