using UnityEngine;

namespace Shooter.Views
{
    public abstract class View : MonoBehaviour
    {
        public View ParentView { get; private set; }

        protected virtual void Awake()
        {
            foreach (var view in GetComponentsInChildren<View>(true))
            {
                if (view == this)
                {
                    continue;
                }
                
                view.SetParentView(this);
            }
        }

        public void SetParentView(View view)
        {
            ParentView = view;
        }

        public View GetMainView()
        {
            var result = this;
            
            while (result.ParentView != null)
            {
                result = result.ParentView;
            }

            return result;
        }
    }
}