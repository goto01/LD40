using Constrollers;
using UnityEngine;

namespace Core.Movement
{
    public abstract class BaseMovementObject : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private Transform _transform;

        protected abstract Vector3 Direction { get; }
        protected Vector3 Offset { get { return Direction * _speed*Time.deltaTime; } }
        
        public virtual void UpdateSelf()
        {
            _transform.position += Offset;
        }

        protected virtual void OnEnable()
        {
            _transform = transform;
            MovementController.Instance.Register(this);
        }
        
        protected virtual void OnDestroy()
        {
            if (!MovementController.WasDestoyed)
                MovementController.Instance.Remove(this);
        }

        protected virtual void OnDisable()
        {
            if (!MovementController.WasDestoyed)
                MovementController.Instance.Remove(this);
        }
    }
}
