using System;
using System.Collections.Generic;
using UnityEngine;

namespace Staff.Pool
{
    public class Pool : MonoBehaviour
    {
        [SerializeField] private PoolableObject _source;
        [SerializeField] private int _poolSize = 10;
        [SerializeField] private List<PoolableObject> _pool;

        private int objectIndex;

        protected virtual void Awake()
        {
            CreateObjects(_poolSize);
        }

        public PoolableObject Pop()
        {
            UpdatePool();
            var lastIndex = _pool.Count - 1;
            var @object = _pool[lastIndex];
            _pool.RemoveAt(lastIndex);
            @object.Disabled += ObjectDisabled;
            @object.gameObject.SetActive(true);
            return @object;
        }

        public T Pop<T>()
        {
            return Pop().GetComponent<T>();
        }

        public void Dispose(PoolableObject @object)
        {
            @object.Deactivate();
        }

        public void Dispose<T>(T @object) where T:MonoBehaviour
        {
            Dispose(@object.GetComponent<PoolableObject>());
        }

        private void UpdatePool()
        {
            if (_pool.Count == 0) 
            {
                _poolSize += _poolSize;
                CreateObjects(_poolSize);
            }
        }

        private void CreateObjects(int size)
        {
            for (var index = 0; index < size; index++)
            {
                var @object = Instantiate(_source);
                @object.transform.parent = transform;
                @object.gameObject.SetActive(false);
                @object.name = string.Format("{0} #{1}", _source.name, ++objectIndex);
                _pool.Add(@object);
            }
        }
        
        private void ObjectDisabled(object sender, EventArgs e)
        {
            var @object = sender as PoolableObject;
            @object.Disabled -= ObjectDisabled;
            _pool.Add(@object);
        }
    }
}
