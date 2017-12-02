﻿using UnityEngine;

namespace Core.Singleton
{
    public abstract class SingletonMonoBahaviour<T> : MonoBehaviour, ISingletonMonoBehaviour where T: SingletonMonoBahaviour<T>, ISingletonMonoBehaviour
    {
        public static bool WasDestoyed { get; private set; }

        public static T Instance
        {
            get
            {
                return WasDestoyed ? null : UnitySingleton<T>.Instance;
            }
        }

        protected void Awake()
        {
            UnitySingleton<T>.Awake(this as T);
        }

        protected void OnDestroy()
        {
            WasDestoyed = true;
        }
        
        public abstract void AwakeSingleton();
    }
}
