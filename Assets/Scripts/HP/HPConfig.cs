using UnityEngine;

namespace Shooter.HP
{
    [CreateAssetMenu(fileName = nameof(HPConfig), menuName = "Configs/" + nameof(HPConfig), order = 0)]
    public class HPConfig : ScriptableObject
    {
        public float InitialHP;
        public float MaxHP;
    }
}