using Shooter.Inventory.Core;
using UnityEngine;

namespace Shooter.Player
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/" + nameof(PlayerConfig), order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerModel PlayerModel;
        public InventoryConfig InventoryConfig;
    }
}