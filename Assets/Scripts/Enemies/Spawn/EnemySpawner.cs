using System;
using System.Collections.Generic;
using DI.Attributes;
using Shooter.Controllers;
using Shooter.Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Shooter.Enemies.Spawn
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig[] enemyConfigs;

        [Inject] private ObjectsViewsFactory viewsFactory;

        private Stack<IController> freeControllers = new();
        private Stack<IController> usingControllers = new();
        
        public void SpawnEnemy()
        {
            var configIndex = Random.Range(0, enemyConfigs.Length);
            var config = enemyConfigs[configIndex];
            var view = viewsFactory.GetView(config);
            var controller = (IController)Activator.CreateInstance(config.GetControllerType());
        }
    }
}