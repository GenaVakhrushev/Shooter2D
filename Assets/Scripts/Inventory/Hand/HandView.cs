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

        [Inject] private ItemsFactory itemsFactory;
        
        private HandModel model;
        private ItemView currentItemView;

        public void SetModel(HandModel newModel)
        {
            if (model != null)
            {
                model.ItemChanged -= ModelOnItemChanged;
            }
            
            model = newModel;
            
            if (model != null)
            {
                SetItem(model.HeldItem);
                model.ItemChanged += ModelOnItemChanged;
            }
        }

        private void ModelOnItemChanged(Item item)
        {
            SetItem(item);
        }

        private void SetItem(Item item)
        {
            if (currentItemView != null)
            {
                itemsFactory.ReturnView(currentItemView);
            }

            if (item == null)
            {
                currentItemView = null;
                return;
            }
            
            currentItemView = itemsFactory.GetItemView(item);
            currentItemView.SetParentView(this);
            
            var viewTransform = currentItemView.transform;

            viewTransform.parent = itemParent;
            viewTransform.position = itemParent.position;
            viewTransform.rotation = itemParent.rotation;
        }
    }
}