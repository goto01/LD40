using Constrollers;
using Constrollers.Input;
using UnityEngine;

namespace Core
{
    class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _actualHealth;

        private Vector3 Direction
        {
            get
            {
                return InputController.Instance.GetPointerDirectionFrom(transform.position);
            }
        }

        public virtual void UpdateSelf()
        {
            if (InputController.Instance.GetShotButtonDown())
                MakeShot();
        }

        private void MakeShot()
        {
            WeaponController.Instance.MakePlayerShot(transform.position, Direction);
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + Direction);
        }
#endif
    }
}
