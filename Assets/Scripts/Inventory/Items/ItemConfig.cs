using TopDownShooter.Configs;
using UnityEngine;

namespace Shooter.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Configs/" + nameof(ItemConfig), order = 0)]
    public class ItemConfig : ObjectConfig<IItem>
    {
        
    }
}