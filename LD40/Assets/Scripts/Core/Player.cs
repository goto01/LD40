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
