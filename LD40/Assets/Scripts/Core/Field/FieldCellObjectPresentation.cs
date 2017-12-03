using System.Collections;
using UnityEngine;

namespace Core.Field
{
    [RequireComponent(typeof(BaseFieldCellObject))]
    class FieldCellObjectPresentation : MonoBehaviour
    {
        private const string CrackTex = "_CrackTex";

        [SerializeField] private Transform _rendererRoot;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Texture2D _noneCrackTexture;
        [SerializeField] private Texture2D _crackTexture0;
        [SerializeField] private Texture2D _crackTexture1;
        [Header("Shake Settings")]
        [SerializeField] private float _shakeStartDuration = 0.1f;
        [SerializeField] private float _shakeTimeRatio = 1.0f;
        [SerializeField] private float _shakeAmplitude = 1.0f;

        private BaseFieldCellObject _fieldCellObject;
        private MaterialPropertyBlock _materialPropertyBlock;
        private Coroutine _shakeCoroutine;

        private Texture2D CurrentCrackTexture
        {
            get
            {
                if (_fieldCellObject.WeightDelta > .75) return _noneCrackTexture;
                if (_fieldCellObject.WeightDelta > .25f) return _crackTexture0;
                return _crackTexture1;
            }
        }

        protected virtual void Awake()
        {
            _fieldCellObject = GetComponent<BaseFieldCellObject>();
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        protected virtual void Update()
        {
            _spriteRenderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetTexture(CrackTex, CurrentCrackTexture);
            _spriteRenderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void StartShake()
        {
            if (_shakeCoroutine != null) return;
            _shakeCoroutine = StartCoroutine(Shake());
        }

        public void StopShake()
        {
            if (_shakeCoroutine == null) return;
            StopCoroutine(_shakeCoroutine);
            _shakeCoroutine = null;
            _rendererRoot.transform.localPosition = Vector3.zero;
        }

        private IEnumerator Shake()
        {
            var t = 0.0f;
            var a = Random.insideUnitCircle * _shakeAmplitude;
            var b = -a;
            var startOffset = Random.insideUnitCircle;
            while (true)
            {
                var position = _rendererRoot.transform.localPosition;
                position.x = Mathf.Lerp(a.x, b.x, Mathf.PingPong((t + startOffset.x) * _shakeTimeRatio, 1.0f));
                position.y = 0.0f;
                position.z = Mathf.Lerp(a.y, b.y, Mathf.PingPong((t + startOffset.y) * _shakeTimeRatio, 1.0f));
                _rendererRoot.transform.localPosition =
                    position * (Mathf.Min(_shakeStartDuration, t) / _shakeStartDuration);
                yield return null;
                t += Time.deltaTime;
            }
        }
    }
}