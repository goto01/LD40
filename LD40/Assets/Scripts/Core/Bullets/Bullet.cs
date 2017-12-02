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

        protected override Vector3 Direction
        {
            get { return _direction; }
        }

        protected virtual void Awake()
        {
            _poolableObject = GetComponent<PoolableObject>();
        }

        public void Init(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            _direction = direction;
        }

        public override void UpdateSelf()
        {
            UpdateRaycasting();
            base.UpdateSelf();
        }

        private void UpdateRaycasting()
        {
            if (Physics.Raycast(transform.position, Direction, Offset.magnitude, _layerMask))
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
