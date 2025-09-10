using Shooter.Inventory.Items.Weapons;
using UnityEngine;

namespace Shooter.Inventory.Items
{
    public abstract class ItemConfig : ScriptableObject
    {
        public string ItemName;
        public ItemView ItemView;

        public abstract Item CreateItem();
    }
}