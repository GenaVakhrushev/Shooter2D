using System;
using Shooter.Views;

namespace Shooter.Controllers
{
    public abstract class Controller<TModel, TView> : IController where TView : View
    {
        public TModel Model { get; private set; }
        public TView View { get; private set; }
        
        public virtual void SetModel(TModel model)
        {
            Model = model;
        }
        
        public virtual void SetView(TView view)
        {
            View = view;
        }

        public Type GetModelType() => typeof(TModel);
        public Type GetViewType() => typeof(TView);
        
        public void SetModel(object model)
        {
            if (model is not TModel tModel)
            {
                throw new ArgumentException("Wrong model type");
            }
            
            SetModel(tModel);
        }

        public void SetView(object view)
        {
            if (view is not TView tView)
            {
                throw new ArgumentException("Wrong view type");
            }
            
            SetModel(tView);
        }
    }
}