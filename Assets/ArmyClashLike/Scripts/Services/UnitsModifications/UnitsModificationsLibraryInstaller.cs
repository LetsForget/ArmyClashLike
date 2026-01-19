using UnityEngine;
using Zenject;

namespace ArmyClashLike.Gameplay
{
    public class UnitsModificationsLibraryInstaller : MonoInstaller
    {
        [SerializeField] private UnitsModificationsLibrary library;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SOUnitsModificationsLibrary>().AsSingle().WithArguments(library);
        }
    }
}