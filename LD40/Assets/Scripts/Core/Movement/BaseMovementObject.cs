﻿using Constrollers;
using Core.Field;
using UnityEngine;

namespace Core.Movement
{
    public abstract class BaseMovementObject : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;

        private BaseWeightyObject baseWeightyObject;
        private int RunParameter = Animator.StringToHash("Running");

        protected abstract Vector3 Direction { get; }
        protected Vector3 Offset { get { return Direction * _speed*Time.deltaTime; } }
        
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
