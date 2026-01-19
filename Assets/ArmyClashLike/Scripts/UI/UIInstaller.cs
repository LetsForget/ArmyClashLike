using UI;
using UnityEngine;
using Zenject;

namespace ArmyClashLike.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIContainer uiContainer;
        [SerializeField] private UIConfig uiConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIManager<ArmyClashFK>>().AsSingle().WithArguments(uiContainer, uiConfig);
        }
    }
}