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

        public void UseItem()
        {
            var heldItemConfig = GetHeldItemConfig();

            if (heldItemConfig)
            {
                heldItemConfig.Object.Use();
            }
        }
        
        public ItemConfig GetHeldItemConfig() => Model.HeldItemConfig;
        
        public void TakeItem(ItemConfig itemConfig) => Model.SetItemConfig(itemConfig);

        public void DropItem() => Model.ResetItem();
    }
}