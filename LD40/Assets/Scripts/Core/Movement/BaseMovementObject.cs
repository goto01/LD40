using Assets.Scripts.Constrollers;
using UnityEngine;

namespace Assets.Scripts.Core.Movement
{
    class BaseMovementObject : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _direction;
        [SerializeField] private Transform _transform;

        private Vector3 Offset { get { return _direction*_speed*Time.deltaTime; } }

        protected virtual void Awake()
        {
            _transform = transform;
            MovementController.Instance.Register(this);
        }

        public virtual void UpdateSelf()
        {
            _transform.position += Offset;
        }
    }
}
