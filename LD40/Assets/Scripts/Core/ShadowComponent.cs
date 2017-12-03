using Core.Enemies;
using UnityEngine;

namespace Core
{
    class ShadowComponent : MonoBehaviour
    {
        [SerializeField] private float _actualHeight;
        [SerializeField] private Enemy _enemy;
        private Vector3 _origin;

        protected virtual void Awake()
        {
            _actualHeight = transform.position.y - .05f;
            _origin = transform.localPosition;
        }

        protected virtual void Update()
        {
            if (_enemy.Spawning)
            {
                var pos = transform.position;
                pos.y = _actualHeight;
                transform.position = pos;
            }
            else
            {
                transform.localPosition = _origin;
            }
        }
    }
}
