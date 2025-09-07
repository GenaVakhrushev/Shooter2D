using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    public abstract class MeleeWeapon : Weapon
    {
        [SerializeField] private float hitDistance;
        [SerializeField] private float range;

        public event Action Striked;
        
        public override void Use()
        {
            Strike();
        }

        private void Strike()
        {
            Striked?.Invoke();
        }
    }
}