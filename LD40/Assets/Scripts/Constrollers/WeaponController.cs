using Constrollers.Input;
using Core.Bullets;
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

        public void MakePlayerShot(Vector3 position, Vector3 direction)
        {
            MakeShot(position, direction, PlayerController.Instance.PlayerBulletsLayerMask);
        }

        public void MakeShot(Vector3 position, Vector3 direction, LayerMask layerMask)
        {
            var bullet = _bullets.Pop<Bullet>();
            bullet.Init(position, direction, _maxDistance, layerMask);
        }
    }
}
