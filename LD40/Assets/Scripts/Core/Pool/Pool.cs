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

        protected virtual void Awake()
        {
            CreateObjects(_poolSize);
        }

        public PoolableObject Pop()
        {
            UpdatePool();
            var @object = _pool[0];
            _pool.RemoveAt(0);
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
            @object.GetComponent<PoolableObject>().Deactivate();
        }

        private void UpdatePool()
        {
            if (_pool.Count == 0) CreateObjects(_poolSize);
        }

        private void CreateObjects(int size)
        {
            for (var index = 0; index < size; index++)
            {
                var @object = Instantiate(_source);
                @object.transform.parent = transform;
                @object.gameObject.SetActive(false);
                @object.name = _source.name;
                _pool.Add(@object);
            }
            _poolSize *= size;
        }
        
        private void ObjectDisabled(object sender, EventArgs e)
        {
            var @object = sender as PoolableObject;
            @object.Disabled -= ObjectDisabled;
            _pool.Add(@object);
        }
    }
}
