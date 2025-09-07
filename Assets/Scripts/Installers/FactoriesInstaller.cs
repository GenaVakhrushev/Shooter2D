using DI.Installers;
using Shooter.Databases;
using Shooter.Factories;
using UnityEngine;

namespace Shooter.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private ItemsDatabase itemsDatabase;
        [SerializeField] private BulletsDatabase bulletsDatabase;
        
        public override void InstallBindings()
        {
            container.Bind(_ => new ItemsFactory(itemsDatabase));
            container.Bind(_ => new BulletsFactory(bulletsDatabase));
        }
    }
}