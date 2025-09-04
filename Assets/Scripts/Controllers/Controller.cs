using System;
using Shooter.Views;

namespace Shooter.Controllers
{
    public abstract class Controller<TModel, TView> where TView : View
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
    }
}