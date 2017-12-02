using Constrollers.Input;
using Core.Bullets;
using Core.Weapons;
using Staff.Pool;
using UnityEngine;

namespace Constrollers
{
    class WeaponController : BaseController<WeaponController>
    {
        [SerializeField] private Pool _bullets;
        [SerializeField] private float _maxDistance = 100;

        public override void AwakeSingleton()
        {
        }

        public void Update()
        {
        }

        public void MakePlayerShot(Weapon weapon, Vector3 position, Vector3 direction)
        {
            MakeShot(weapon, position, direction, PlayerController.Instance.PlayerBulletsLayerMask);
        }

        public void MakeShot(Weapon weapon, Vector3 position, Vector3 direction, LayerMask layerMask)
        {
            for (var index = 0; index < weapon.BulletsPerShot; index++)
            {
                var bullet = _bullets.Pop<Bullet>();
                bullet.Init(position, Quaternion.Euler(0, weapon.RandomAngleRange, 0)*direction, weapon.BulletSpeed, _maxDistance, weapon.WeightValue, layerMask);
            }
        }
    }
}
