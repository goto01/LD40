using UnityEngine;

namespace Core.Field
{
    [RequireComponent(typeof(BaseFieldCellObject))]
    class FieldCellObjectPresentation : MonoBehaviour
    {
        private const string CrackTex = "_CrackTex";

        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Texture2D _noneCrackTexture;
        [SerializeField] private Texture2D _crackTexture0;
        [SerializeField] private Texture2D _crackTexture1;
        [SerializeField] private Texture2D _crackTexture2;
        private BaseFieldCellObject _fieldCellObject;

        private Texture2D CurrentCrackTexture
        {
            get
            {
                if (_fieldCellObject.WeightDelta < Mathf.Epsilon) return _noneCrackTexture;
                if (_fieldCellObject.WeightDelta < .50f) return _crackTexture0;
                if (_fieldCellObject.WeightDelta < .75f) return _crackTexture1;
                return _crackTexture2;
            }
        }

        private Material Material { get { return _meshRenderer.material; } }

        protected virtual void Awake()
        {
            _fieldCellObject = GetComponent<BaseFieldCellObject>();
        }

        protected virtual void Update()
        {
            Material.SetTexture(CrackTex, CurrentCrackTexture);
        }
    }
}
