using System.Collections;
using Constrollers;
using Constrollers.Input;
using Controllers;
using Core.Movement;
using Core.Weapons;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(WeaponComponent),
        typeof(PlayerMovementObject))]
    class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _actualHealth;
        [SerializeField] private Transform _actualTransform;
        [SerializeField] private Transform _gun;
        [SerializeField] private Animator _bulletSpawnerAnimator;
        [SerializeField] private Transform _bulletSpawnerTransform;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private bool _dashing;
        [SerializeField] private float _dashDistance;
        [SerializeField] private Animator _spriteAnimator;
        private PlayerMovementObject _movementObject;
        private WeaponComponent _weaponComponent;
        private int _shotParameter = Animator.StringToHash("Shot");
        private int _dashParameter = Animator.StringToHash("Dash");

        private Vector3 Direction
        {
            get
            {
                return InputController.Instance.GetPointerDirectionFrom(transform.position);
            }
        }

        protected virtual void Awake()
        {
            _movementObject = GetComponent<PlayerMovementObject>();
            _weaponComponent = GetComponent<WeaponComponent>();
        }

        public virtual void UpdateSelf()
        {
            if (!_dashing && InputController.Instance.GetDashButtonDown()) StartCoroutine(DashCoroutine());
            if (_dashing) return;
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
            if (_weaponComponent.Shot(_bulletSpawnerTransform.position, Direction,
                PlayerController.Instance.PlayerBulletsLayerMask))
                _bulletSpawnerAnimator.SetTrigger(_shotParameter);
        }

        private IEnumerator DashCoroutine()
        {
            _spriteAnimator.SetBool(_dashParameter, (_dashing = true));
            var speed = _movementObject.Speed;
            _movementObject.Speed = _dashSpeed;
            _movementObject.BlockGettingDirection = true;
            var startPos = transform.position;
            while (Vector3.Distance(startPos, transform.position) < _dashDistance) yield return null;
            _movementObject.Speed = speed;
            _movementObject.BlockGettingDirection = false;
            _spriteAnimator.SetBool(_dashParameter, (_dashing = false));
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
