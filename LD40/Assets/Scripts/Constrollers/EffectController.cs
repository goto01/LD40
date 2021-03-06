﻿using System.Collections;
using System.ComponentModel;
using Constrollers;
using Core;
using Staff.Pool;
using UnityEngine;

namespace Controllers
{
    class EffectController : BaseController<EffectController>
    {
        [SerializeField] private float _screenShakeAngle;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private AnimationCurve _screenShakeAnimationCurve;
        [SerializeField] private float _screenShakeHalfAngle;
        [SerializeField] private bool _shaking;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private CameraPostEffectComponent _cameraPostEffectComponent;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private Pool _ghostPool;
        [SerializeField] private Pool _plusPool;
        [SerializeField] private float _plusSpawnDelay;
        [SerializeField] private float _plusSpawnTimeStamp;
        [SerializeField] private Pool _minusPool;
        [SerializeField] private float _minusSpawnDelay;
        [SerializeField] private float _minusSpawnTimeStamp;

        public float FadeDuration { get { return _fadeDuration; } }

        public override void AwakeSingleton()
        {
            _cameraTransform = UnityEngine.Camera.main.transform;
            _screenShakeHalfAngle = _screenShakeAngle/2f;
        }

        public void Shake()
        {
            if (_shaking) return;
            StartCoroutine(Shake(_shakeDuration));
        }

        public void FadeIn()
        {
            StartCoroutine(FadeCoroutine(-.1f, 1.1f));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeCoroutine(1.1f, -.1f));
        }

        public void SpawnGhost(Vector3 position, Sprite sprite, Vector3 scale)
        {
            var spriteRenderer = _ghostPool.Pop<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.position = position;
            spriteRenderer.transform.localScale = scale;
        }

        public void SpawnPlus(Vector3 position)
        {
            if (_plusSpawnTimeStamp + _plusSpawnDelay > Time.time) return;
            _plusSpawnTimeStamp = Time.time;
            _plusPool.Pop<Transform>().position = position;
        }

        public void SpawnMinus(Vector3 position)
        {
            if (_minusSpawnTimeStamp + _minusSpawnDelay > Time.time) return;
            _minusSpawnTimeStamp = Time.time;
            _minusPool.Pop<Transform>().position = position;
        }

        private IEnumerator FadeCoroutine(float from, float to)
        {
            var startTime = Time.time;
            while (startTime + _fadeDuration > Time.time)
            {
                _cameraPostEffectComponent.FadeDleta = Mathf.Lerp(from, to, (Time.time - startTime)/_fadeDuration);
                //Debug.Log(Mathf.Lerp(from, to, (Time.time - startTime) / _fadeDuration));
                yield return null;
            }
            _cameraPostEffectComponent.FadeDleta = to;
        }

        private IEnumerator Shake(float duration)
        {
            _shaking = true;
            var startTime = Time.time;
            var finishTime = startTime + duration;
            while (finishTime > Time.time)
            {
                var oldRotation = _cameraTransform.rotation;
                var delta = _screenShakeAnimationCurve.Evaluate(Time.time - startTime);
                var actualShakeAngle = Mathf.Lerp(0, _screenShakeHalfAngle, delta);
                _cameraPostEffectComponent.RGBOffsetDelta = delta;
                _cameraTransform.RotateAround(_cameraTransform.position, _cameraTransform.forward, Random.Range(-actualShakeAngle, actualShakeAngle));
                yield return null;
                _cameraTransform.rotation = oldRotation;
            }
            _shaking = false;
            _cameraPostEffectComponent.RGBOffsetDelta = 0;
        }
    }
}
