using Common;
using Common.JsonConverting;
using Common.Storage;
using ContentLoading;
using Logging.UnityConsoleLogging;
using Zenject;

namespace ArmyClashLike
{
    public class CommonServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CommonFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AddressablesContentLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityLogsWriter>().AsSingle();

            Container.BindInterfacesAndSelfTo<LocalStorageProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityJsonConverter>().AsSingle();
        }
    }
}