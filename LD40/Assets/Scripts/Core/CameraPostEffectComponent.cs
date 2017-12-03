using UnityEngine;

namespace Core
{
    class CameraPostEffectComponent : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private float _rgbOffsetMinValue;
        [SerializeField] private float _rgbOffsetMaxValue;
        [SerializeField] private float _rgbOffsetDelta;
        private string _rgbOffsetDeltaParameter = "_RGBOffsetDelta";
        private string _fadeDelta = "_FadeDelta";

        public float RGBOffsetDelta
        {
            get { return _rgbOffsetDelta; }
            set { _rgbOffsetDelta = value; }
        }

        public float FadeDleta { set { _material.SetFloat(_fadeDelta, value);} }

        protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            _material.SetFloat(_rgbOffsetDeltaParameter, Mathf.Lerp(_rgbOffsetMinValue, _rgbOffsetMaxValue, _rgbOffsetDelta));
            Graphics.Blit(src, dst, _material);
        }
    }
}
