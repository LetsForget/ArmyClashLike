using ArmyClashLike.Gameplay.Move;
using UnityEngine;
using Zenject;

namespace ArmyClashLike.Gameplay
{
    public class UnitControllerInstaller : MonoInstaller
    {
        [SerializeField] private float searchCoolDown;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ParallelJobSearcher>().AsSingle().WithArguments(searchCoolDown);
            Container.BindInterfacesAndSelfTo<NavMeshUnitMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnitAttacker>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<UnitsController>().AsSingle();
        }
    }
}