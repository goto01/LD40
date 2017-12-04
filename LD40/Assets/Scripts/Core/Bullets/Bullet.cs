using Constrollers;
using Controllers;
using Core.Field;
using Core.Movement;
using Staff.Pool;
using UnityEngine;

namespace Core.Bullets
{
    [RequireComponent(typeof(PoolableObject))]
    class Bullet : BaseMovementObject
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private Staff.Pool.PoolableObject _poolableObject;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private float _maxDistance;
        [SerializeField] private int _weightValue;
        [SerializeField] private float _radius;
        
        private int _destroyTrigger = Animator.StringToHash("Destroy");

        protected override Vector3 Direction
        {
            get { return _direction; }
        }

        protected virtual void Awake()
        {
            _poolableObject = GetComponent<PoolableObject>();
        }

        public void Init(Vector3 position, Vector3 direction, float speed, float maxDistance, int weightValue, LayerMask mask)
        {
            _weightValue = weightValue;
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
            RaycastHit ray;
            if (Physics.SphereCast(transform.position, _radius, Direction, out ray, Offset.magnitude, _layerMask))
            {
                //_poolableObject.Deactivate();
                _animator.SetTrigger(_destroyTrigger);
                EffectController.Instance.Shake();
                var movementObject = ray.collider.GetComponent<BaseMovementObject>();
                if (movementObject != null)
                {
                    movementObject.OnWasShooted();
                }
                AudioController.Play("ShotDestroyed");
                WeightController.Instance.GiveWeightToObject(_weightValue, ray.collider.GetComponent<BaseWeightyObject>());
                return;
            }
            if (Vector3.Distance(transform.position, _startPosition) > _maxDistance)
            {
                _poolableObject.Deactivate();
                AudioController.Play("ShotDestroyed");

            }
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Offset);
        }
#endif
    }
}
