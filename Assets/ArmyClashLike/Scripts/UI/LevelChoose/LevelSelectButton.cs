using System;
using Common.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArmyClashLike.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelSelectButton : MonoBehaviour, IPoolable
    {
        public event Action<int> ButtonPressed;

        [SerializeField] private TMP_Text levelLabel;
        [SerializeField] private Button button;
        
        private int index = -1;
        
        private void Awake()
        {
            button.onClick.AddListener(delegate { ButtonPressed?.Invoke(index); });
        }

        public void Set(int index, string label)
        {
            this.index = index;
            levelLabel.text = label;
        }
        
        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void OnTakenFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnTakenBackToPool()
        {
            gameObject.SetActive(false);
        }
    }
}