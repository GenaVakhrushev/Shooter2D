using UnityEngine;

namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    public class MeleeWeaponView : ItemView
    {
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
    }
}