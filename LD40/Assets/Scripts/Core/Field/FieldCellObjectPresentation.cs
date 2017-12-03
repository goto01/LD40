using UnityEngine;

namespace Core.Field
{
    [RequireComponent(typeof(BaseFieldCellObject))]
    class FieldCellObjectPresentation : MonoBehaviour
    {
        private const string CrackTex = "_CrackTex";

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Texture2D _noneCrackTexture;
        [SerializeField] private Texture2D _crackTexture0;
        [SerializeField] private Texture2D _crackTexture1;
        private BaseFieldCellObject _fieldCellObject;
        private MaterialPropertyBlock _materialPropertyBlock;

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
    }
}
