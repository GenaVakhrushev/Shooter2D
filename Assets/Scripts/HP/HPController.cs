using System;
using Shooter.Controllers;

namespace Shooter.HP
{
    public class HPController : Controller<HPModel, HPView>
    {
        public event Action LostHP;

        public override void SetModel(HPModel model)
        {
            if (Model != null)
            {
                Model.HpChanged -= ModelOnHpChanged;
            }
            
            base.SetModel(model);
            if (View != null)
            {
                View.SetModel(Model);
            }

            if (Model != null)
            {
                Model.HpChanged += ModelOnHpChanged;
            }
        }

        public override void SetView(HPView view)
        {
            base.SetView(view);

            if (View != null)
            {
                View.SetModel(Model);
            }
        }

        private void ModelOnHpChanged(float hp)
        {
            if (hp <= 0)
            {
                LostHP?.Invoke();
            }
        }

        public void AddHP(float amount)
        {
            Model.SetHP(Model.CurrentHP + amount);
        }
        
        public void RemoveHP(float amount)
        {
            Model.SetHP(Model.CurrentHP - amount);
        }
    }
}