using System;
using System.Collections.Generic;
using Shooter.Enemies;
using UnityEngine.Pool;

namespace Shooter.Services
{
    public class EnemiesService
    {
        private readonly HashSet<EnemyController> activeControllers = new();
        private readonly Dictionary<Type, ObjectPool<EnemyController>> pools = new();
        
        public EnemyController GetController<T>() where T : EnemyController => GetOrCreateControllerPool(typeof(T)).Get();

        public void ReturnController(EnemyController enemyController) => GetOrCreateControllerPool(enemyController.GetType()).Release(enemyController);

        public void KillAll()
        {
            var controllersCopy = new HashSet<EnemyController>(activeControllers);
            foreach (var controller in controllersCopy)
            {
                controller.Die();
            }
        }

        public void StopEnemies()
        {
            foreach (var controller in activeControllers)
            {
                controller.SetAttackTarget(null);
            }
        }
        
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
                var enemyController = new EnemyController();

                enemyController.EnemyDied += ReturnController;
                
                return enemyController;
            }

            void ActionOnGetController(EnemyController controller)
            {
                activeControllers.Add(controller);
            }
            
            void ActionOnReleaseController(EnemyController controller)
            {
                activeControllers.Remove(controller);
                
                controller.SetView(null);
                controller.SetModel(null);
            }
        }
    }
}