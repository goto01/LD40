using Constrollers;
using UnityEngine;

namespace Core.Weapons
{
    class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _lastShotTime;

        private bool ShotPossible { get { return _lastShotTime + _weapon.ShotDelay < Time.time; } }

        public void Shot(Vector3 position, Vector3 direction, LayerMask mask)
        {
            if (!ShotPossible) return;
            _lastShotTime = Time.time;
            WeaponController.Instance.MakeShot(_weapon, position, direction, mask);
        }
    }
}
