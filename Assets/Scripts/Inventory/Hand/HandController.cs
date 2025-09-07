using Shooter.Controllers;
using Shooter.Inventory.Items;

namespace Shooter.Inventory.Hand
{
    public class HandController : Controller<HandModel, HandView>
    {
        public override void SetModel(HandModel model)
        {
            base.SetModel(model);

            if (View)
            {
                View.SetModel(Model);
            }
        }

        public override void SetView(HandView view)
        {
            base.SetView(view);

            if (View)
            {
                View.SetModel(Model);
            }
        }

        public void UseItem() => GetHeldItem()?.Use();

        public Item GetHeldItem() => Model.HeldItem;
        
        public void TakeItem(Item item) => Model.SetItem(item);

        public void DropItem() => Model.ResetItem();
    }
}