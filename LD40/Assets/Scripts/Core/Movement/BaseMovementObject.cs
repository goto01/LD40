﻿using System.Collections;
using Constrollers;
using Core.Field;
using UnityEngine;

namespace Core.Movement
{
    public abstract class BaseMovementObject : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer[] _renderers;
        [SerializeField] private float _weightToSpeedRatio = 1;
        [SerializeField] private Color _shotFlashColor = Color.black;
        [SerializeField] private float _shotFlashDuration = 0.5f;

        private BaseWeightyObject baseWeightyObject;
        private int RunParameter = Animator.StringToHash("Running");
        private Coroutine flashCoroutine;

        public float Speed
        {
            get
            {
                return baseWeightyObject == null
                    ? _speed
                    : _speed / baseWeightyObject.CurrentWeight * _weightToSpeedRatio;
            }
            set { _speed = value; }
        }

        protected abstract Vector3 Direction { get; }

        protected Vector3 Offset
        {
            get { return Direction * Speed * Time.deltaTime; }
        }

        public virtual void UpdateSelf()
        {
            var offset = Offset;
            _transform.position += offset;
            UpdateAnimator(offset.magnitude);
            if (baseWeightyObject != null)
                baseWeightyObject.OnMoved(offset);
        }

        public void OnWasShooted()
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(Flash(_shotFlashColor));
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

        private IEnumerator Flash(Color startColor)
        {
            var finishColor = Color.white;
            for (float t = 0.0f; t < _shotFlashDuration; t += Time.deltaTime)
            {
                foreach (var renderer in _renderers)
                {
                    renderer.color = Color.Lerp(startColor, finishColor, t / _shotFlashDuration);
                }
                yield return null;
            }
            foreach (var renderer in _renderers)
            {
                renderer.color = finishColor;
            }
            flashCoroutine = null;
        }
    }
}