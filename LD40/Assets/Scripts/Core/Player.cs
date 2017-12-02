using Constrollers;
using Constrollers.Input;
using Core.Weapons;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(WeaponComponent))]
    class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _actualHealth;
        [SerializeField] private Transform _actualTransform;
        [SerializeField] private Transform _gun;
        private WeaponComponent _weaponComponent;

        private Vector3 Direction
        {
            get
            {
                return InputController.Instance.GetPointerDirectionFrom(transform.position);
            }
        }

        protected virtual void Awake()
        {
            _weaponComponent = GetComponent<WeaponComponent>();
        }

        public virtual void UpdateSelf()
        {
            if (InputController.Instance.GetShotButtonPressed())
                MakeShot();
            UpdateScale();
        }

        private void UpdateScale()
        {
            var dir = Quaternion.Euler(0, 45, 0)*Direction;
            var scale = _actualTransform.localScale;
            scale.x = -Mathf.Sign(dir.x);
            _actualTransform.localScale = scale;
            _gun.localEulerAngles = Quaternion.FromToRotation(Vector3.right, new Vector3(dir.x * Mathf.Sign(dir.x), -dir.z, 0)).eulerAngles;
        }

        float Angle360(Vector3 v1, Vector3 v2, Vector3 n)
        {
            float angle = Vector3.Angle(v1, v2);
            
            float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(v1, v2)));
            float signed_angle = angle * sign;
            
            return (signed_angle + 180) % 360;
        }

        private void MakeShot()
        {
            _weaponComponent.Shot(transform.position, Direction, PlayerController.Instance.PlayerBulletsLayerMask);
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            Gizmos.DrawLine(transform.position, transform.position + Direction);
        }
#endif
    }
}
