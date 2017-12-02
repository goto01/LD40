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
        [SerializeField] private Animator _bulletSpawnerAnimator;
        [SerializeField] private Transform _bulletSpawnerTransform;
        private WeaponComponent _weaponComponent;
        private int _shotParameter = Animator.StringToHash("Shot");

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

        private void MakeShot()
        {
            if (_weaponComponent.Shot(_bulletSpawnerTransform.position, Direction, PlayerController.Instance.PlayerBulletsLayerMask))
                _bulletSpawnerAnimator.SetTrigger(_shotParameter);
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
