using UnityEngine;

namespace Core
{
    class CameraPostEffectComponent : MonoBehaviour
    {
        [SerializeField] private Material _material;

        protected virtual void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            Graphics.Blit(src, dst, _material);
        }
    }
}
