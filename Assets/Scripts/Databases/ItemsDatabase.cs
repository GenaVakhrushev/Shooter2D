using Shooter.Inventory.Items;
using UnityEngine;

namespace Shooter.Databases
{
    [CreateAssetMenu(fileName = nameof(ItemsDatabase), menuName = "Databases/" + nameof(ItemsDatabase), order = 0)]
    public class ItemsDatabase : ScriptableObject
    {
        public ItemConfig[] ItemConfigs;
    }
}