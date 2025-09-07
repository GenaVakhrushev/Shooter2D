using UnityEngine;

namespace Shooter.Damage.Bullets
{
    [CreateAssetMenu(fileName = nameof(BulletConfig), menuName = "Configs/" + nameof(BulletConfig), order = 0)]
    public class BulletConfig : ScriptableObject
    {
        public string BulletName;
        public BulletView BulletView;
    }
}