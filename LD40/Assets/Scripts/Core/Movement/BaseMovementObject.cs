using Constrollers;
using Core.Field;
using UnityEngine;

namespace Core.Movement
{
    public abstract class BaseMovementObject : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private Transform _transform;
        [SerializeField] protected Animator _animator;
        [SerializeField] private float _weightToSpeedRatio = 1;
        [SerializeField] private bool _useOroginSpeed;

        private BaseWeightyObject baseWeightyObject;
        private int RunParameter = Animator.StringToHash("Running");

        public bool UseOriginSpeed
        {
            get { return _useOroginSpeed; }
            set { _useOroginSpeed = value; }
        }

        public float OrinSpeed
        {
            get { return _speed;}
            set { _speed = value; }
        }

        public float Speed
        {
            get
            {
                return baseWeightyObject == null || _useOroginSpeed ? _speed : _speed/baseWeightyObject.CurrentWeight*_weightToSpeedRatio;
            }
            set { _speed = value; }
        }
        protected abstract Vector3 Direction { get; }
        protected Vector3 Offset { get { return Direction * Speed * Time.deltaTime; } }
        
        public virtual void UpdateSelf()
        {
            var offset = Offset;
            _transform.position += offset;
            UpdateAnimator(offset.magnitude);
            if (baseWeightyObject != null)
                baseWeightyObject.OnMoved(offset);
        }

        private void UpdateAnimator(float offset)
        {
            if (_animator != null)
                _animator.SetBool(RunParameter, offset > Mathf.Epsilon);
        }

        protected virtual void Awake()
        {
            baseWeightyObject = GetComponent<BaseWeightyObject>();
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
