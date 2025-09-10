namespace Shooter.Inventory.Items.Weapons.MeleeWeapons
{
    public class MeleeWeaponView : ItemView
    {
        private MeleeWeapon meleeWeapon;
        private Position currentPosition;

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
                SetPosition(currentPosition);
                meleeWeapon.Striked += MeleeWeapon_OnStrike;
            }
        }

        private void MeleeWeapon_OnStrike()
        {
            SetPosition(currentPosition == Position.Left ? Position.Right : Position.Left);
        }

        private void SetPosition(Position position)
        {
            currentPosition = position;
        }
        
        private enum Position
        {
            Left = 0,
            Right = 1,
        }
    }
}