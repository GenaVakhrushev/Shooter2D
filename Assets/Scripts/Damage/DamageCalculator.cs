using Shooter.Enemies;
using Shooter.Player;
using Shooter.Views;

namespace Shooter.Damage
{
    public class DamageCalculator
    {
        public float CalculateDamage(IDamageDealer dealer, IDamageable damageable)
        {
            if (CanDealDamage(dealer, damageable))
            {
                return dealer.GetDamage();
            }

            return 0;
        }

        private bool CanDealDamage(IDamageDealer dealer, IDamageable damageable)
        {
            var dealerView = dealer as View;
            var damageableView = damageable as View;

            var dealerMainParent = dealerView != null ? dealerView.GetMainView() : null;
            var damageableMainParent = damageableView != null ? damageableView.GetMainView() : null;
            
            if (dealerMainParent is PlayerView && damageableMainParent is PlayerView)
            {
                return false;
            }

            if (dealerMainParent is EnemyView && damageableMainParent is EnemyView)
            {
                return false;
            }

            return true;
        }
    }
}