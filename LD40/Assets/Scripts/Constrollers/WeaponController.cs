using Constrollers.Input;
using Core.Bullets;
using Staff.Pool;
using UnityEngine;

namespace Constrollers
{
    class WeaponController : BaseController<WeaponController>
    {
        [SerializeField] private Pool _bullets;

        public override void AwakeSingleton()
        {
        }

        public void Update()
        {
            if (InputController.Instance.GetShotButtonDown())
            {
                var bullet = _bullets.Pop<Bullet>();
                bullet.Init(Vector3.zero, Vector3.right);
            }
        }
    }
}
