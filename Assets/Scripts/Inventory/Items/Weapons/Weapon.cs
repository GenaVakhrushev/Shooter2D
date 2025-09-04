using System;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons
{
    [Serializable]
    public abstract class Weapon : IItem
    {
        [SerializeField] private float damage = 1;
        
        public abstract void Use();
    }
}