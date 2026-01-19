using UnityEngine;
using Zenject;

namespace ArmyClashLike
{
    public class LevelServiceInstaller : MonoInstaller
    {
        [SerializeField] private LevelsConfig levelsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MultiLevelService>().AsSingle().WithArguments(levelsConfig);
        }
    }
}