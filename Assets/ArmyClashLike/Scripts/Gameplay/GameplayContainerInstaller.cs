using Zenject;

namespace ArmyClashLike.Gameplay
{
    public class GameplayContainerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameplayContainer>().AsSingle();
        }
    }
}