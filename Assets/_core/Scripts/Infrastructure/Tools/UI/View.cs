using UnityEngine;

namespace ZooWorld.UI
{
    public abstract class View : MonoBehaviour
    {
        internal void Open()
        {
            gameObject.SetActive(true);
            OnOpen();
        }

        internal void Close()
        {
            OnClose();
            gameObject.SetActive(false);
        }

        internal void Create()
        {
            OnCreated();
            gameObject.SetActive(false);
        }

        protected virtual void OnCreated()
        {
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }
    }
}
