using System;
using UnityEngine;

namespace GameCore.GeneralExtensions
{
    public class DisposableMonoBehaviour : MonoBehaviour, IDisposable
    {
        protected virtual bool IsOwnerOfGameObject => true;

        public bool HasDisposed { get; private set; }

        public void Dispose()
        {
            if (HasDisposed)
            {
                return;
            }

            HasDisposed = true;
            if (IsOwnerOfGameObject)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        protected virtual void OnDestroy()
        {
            HasDisposed = true;
        }
    }
}


