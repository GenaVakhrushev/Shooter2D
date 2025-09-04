using DI.Attributes;
using Shooter.Factories;
using Shooter.Inventory.Items;
using Shooter.Views;
using UnityEngine;

namespace Shooter.Inventory.Hand
{
    public class HandView : View
    {
        [SerializeField] private Transform itemParent;

        [Inject] private ObjectsViewsFactory factory;
        
        private HandModel model;
        private ItemView currentItemView;

        public void SetModel(HandModel newModel)
        {
            if (model != null)
            {
                model.ItemConfigChanged -= Model_OnItemConfigChanged;
            }
            
            model = newModel;
            
            if (model != null)
            {
                model.ItemConfigChanged += Model_OnItemConfigChanged;
            }
        }

        private void Model_OnItemConfigChanged(ItemConfig itemConfig)
        {
            if (currentItemView != null)
            {
                factory.ReturnView(currentItemView);
            }

            if (itemConfig == null)
            {
                currentItemView = null;
                return;
            }
            
            currentItemView = (ItemView)factory.GetView(itemConfig);
            var viewTransform = currentItemView.transform;

            viewTransform.parent = itemParent;
            viewTransform.position = itemParent.position;
            viewTransform.rotation = itemParent.rotation;
        }
    }
}