using Core.Movement;
using Staff.Pool;
using UnityEngine;

namespace Core.Bullets
{
    [RequireComponent(typeof(PoolableObject))]
    class Bullet : BaseMovementObject
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private PoolableObject _poolableObject;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private float _maxDistance;

        protected override Vector3 Direction
        {
            get { return _direction; }
        }

        protected virtual void Awake()
        {
            _poolableObject = GetComponent<PoolableObject>();
        }

        public void Init(Vector3 position, Vector3 direction, float speed, float maxDistance, LayerMask mask)
        {
            _speed = speed;
            _layerMask = mask;
            transform.position = position;
            _direction = direction;
            _startPosition = position;
            _maxDistance = maxDistance;
        }

        public override void UpdateSelf()
        {
            UpdateForAlive();
            base.UpdateSelf();
        }

        private void UpdateForAlive()
        {
            if (Physics.Raycast(transform.position, Direction, Offset.magnitude, _layerMask))
            {
                _poolableObject.Deactivate();
                return;
            }
            if (Vector3.Distance(transform.position, _startPosition) > _maxDistance)
                _poolableObject.Deactivate();
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Offset);
        }
#endif
    }
}
