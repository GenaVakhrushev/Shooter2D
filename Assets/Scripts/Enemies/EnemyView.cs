using System;
using Shooter.Damage;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Enemies
{
    public class EnemyView : View, IDamageable
    {
        public event Action<float> DamageTaken;
        
        public EnemyModel EnemyModel { get; private set; }

        public void SetEnemyModel(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"{name} take {damage} damage");
            
            DamageTaken?.Invoke(damage);
        }
    }
}