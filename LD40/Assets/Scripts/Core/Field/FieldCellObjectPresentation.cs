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
        [SerializeField] private Sprite _crackSprite0;
        [SerializeField] private Sprite _crackSprite1;
        [Header("Shake Settings")]
        [SerializeField] private float _shakeStartDuration = 0.1f;
        [SerializeField] private float _shakeTimeRatio = 1.0f;
        [SerializeField] private float _shakeAmplitude = 1.0f;

        private BaseFieldCellObject _fieldCellObject;
        private Coroutine _shakeCoroutine;

        public Vector3 ViewOffset
        {
            get { return _rendererRoot.transform.localPosition; }
            private set { _rendererRoot.transform.localPosition = value; }
        }

        private Sprite CurrentCrackSprite
        {
            get
            {
                if (_fieldCellObject.WeightDelta > .75) return null;
                if (_fieldCellObject.WeightDelta > .25f) return _crackSprite0;
                return _crackSprite1;
            }
        }

        protected virtual void Awake()
        {
            _fieldCellObject = GetComponent<BaseFieldCellObject>();
        }

        protected virtual void Update()
        {
            _spriteRenderer.sprite = CurrentCrackSprite;
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
            ViewOffset = Vector3.zero;
        }

        private IEnumerator Shake()
        {
            var t = 0.0f;
            var a = Random.insideUnitCircle * _shakeAmplitude;
            var b = -a;
            var startOffset = Random.insideUnitCircle;
            while (true)
            {
                var position = ViewOffset;
                position.x = Mathf.Lerp(a.x, b.x, Mathf.PingPong((t + startOffset.x) * _shakeTimeRatio, 1.0f));
                position.y = 0.0f;
                position.z = Mathf.Lerp(a.y, b.y, Mathf.PingPong((t + startOffset.y) * _shakeTimeRatio, 1.0f));
                ViewOffset = position * (Mathf.Min(_shakeStartDuration, t) / _shakeStartDuration);
                yield return null;
                t += Time.deltaTime;
            }
        }
    }
}