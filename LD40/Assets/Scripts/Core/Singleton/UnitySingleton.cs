using UnityEngine;

namespace Core.Singleton
{
    public static class UnitySingleton<T> where T : MonoBehaviour, ISingletonMonoBehaviour
    {
        private const string AwakeSingleton = "AwakeSingleton";

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = Init();
                return _instance;
            }
        }

        public static void Awake(T instance)
        {
            if (_instance == instance) return;
            if (_instance != null)
            {
                Debug.LogErrorFormat("Instance of {0} already exist", typeof(T).Name);
                return;
            }
            _instance = instance;
            instance.AwakeSingleton();
        }

        private static T Init()
        {
            Debug.Log(typeof(T).Name);
            var instance = Object.FindObjectOfType<T>();
            if (instance == null)
            {
                Debug.LogError("You don't have instance of " + typeof(T).Name);
                return null;
            }
            instance.AwakeSingleton();
            return instance;
        }
    }
}
