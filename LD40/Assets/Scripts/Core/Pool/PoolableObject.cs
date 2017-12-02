using System;
using Core.Helpers;
using UnityEngine;

namespace Staff.Pool
{
    public class PoolableObject : MonoBehaviour
    {
        public event EventHandler Disabled;

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            Disabled.Raise(this);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
