using Shooter.Models;
using UnityEngine;

namespace Shooter.Configs
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configs/" + nameof(PlayerConfig), order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        public PlayerModel PlayerModel;
    }
}