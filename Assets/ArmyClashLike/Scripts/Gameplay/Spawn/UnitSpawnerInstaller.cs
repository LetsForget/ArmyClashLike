using Zenject;

namespace ArmyClashLike.Gameplay
{
    public class UnitSpawnerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UnitsSpawner>().AsSingle();
        }
    }
}