using System;
using Zenject;

namespace ZooWorld.UI
{
    public abstract class Presenter<TView> : IInitializable, IDisposable where TView : View
    {
        protected readonly TView View;

        protected Presenter(TView view)
        {
            View = view;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}
