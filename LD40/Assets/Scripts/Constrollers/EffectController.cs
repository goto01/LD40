using System.Collections;
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
