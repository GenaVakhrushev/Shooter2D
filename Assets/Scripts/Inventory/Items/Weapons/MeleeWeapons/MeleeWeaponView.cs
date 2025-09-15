using DI.Attributes;
using Shooter.Damage;
using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    public class MeleeWeaponView : ItemView, IDamageDealer
    {
        [Inject] private DamageCalculator damageCalculator;
        
        private MeleeWeapon meleeWeapon;
        private Position currentPosition;

        private void Update()
        {
            if (meleeWeapon == null)
            {
                return;
            }
            
            var targetAngle = currentPosition == Position.Right ? -meleeWeapon.Range : meleeWeapon.Range;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, targetAngle), meleeWeapon.WaveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                var damage = damageCalculator.CalculateDamage(this, damageable);

                if (damage > 0)
                {
                    damageable.TakeDamage(damage);
                }
            }
        }

        public override void SetItem(Item item)
        {
            if (meleeWeapon != null)
            {
                meleeWeapon.Striked -= MeleeWeapon_OnStrike;
            }
            
            base.SetItem(item);
            meleeWeapon = Item as MeleeWeapon;

            if (meleeWeapon != null)
            {
                transform.localRotation = Quaternion.Euler(0, 0, currentPosition == Position.Right ? -meleeWeapon.Range : meleeWeapon.Range);
                meleeWeapon.Striked += MeleeWeapon_OnStrike;
            }
        }

        private void MeleeWeapon_OnStrike()
        {
            currentPosition = currentPosition == Position.Left ? Position.Right : Position.Left;
        }
        
        private enum Position
        {
            Left = 0,
            Right = 1,
        }

        public float GetDamage() => meleeWeapon.Damage;
    }
}