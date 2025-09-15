using System;
using System.Collections.Generic;
using Shooter.Enemies;
using UnityEngine.Pool;

namespace Shooter.Services
{
    public class EnemiesService
    {
        private readonly Dictionary<Type, ObjectPool<EnemyController>> pools = new();
        
        public EnemyController GetController<T>() where T : EnemyController => GetOrCreateControllerPool(typeof(T)).Get();

        public void ReturnController(EnemyController enemyController) => GetOrCreateControllerPool(enemyController.GetType()).Release(enemyController);
        
        private ObjectPool<EnemyController> GetOrCreateControllerPool(Type controllerType)
        {
            if (pools.TryGetValue(controllerType, out var pool))
            {
                return pool;
            }
            
            pool = new ObjectPool<EnemyController>(CreateController, ActionOnGetController, ActionOnReleaseController);
            pools.Add(controllerType, pool);

            return pool;
            
            EnemyController CreateController()
            {
                var controller = new EnemyController();
                controller.EnemyDied += () => ReturnController(controller);
                return controller;
            }

            void ActionOnGetController(EnemyController controller) {}
            void ActionOnReleaseController(EnemyController controller)
            {
                controller.SetView(null);
                controller.SetModel(null);
            }
        }
    }
}