using Constrollers;
using Core.Field;
using UnityEngine;

namespace Core.Weapons
{
    [RequireComponent(typeof(BaseWeightyObject))]
    class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _lastShotTime;

        private BaseWeightyObject baseWeightyObject;

        private void Awake()
        {
            baseWeightyObject = GetComponent<BaseWeightyObject>();
        }

        private bool ShotPossible
        {
            get
            {
                if (baseWeightyObject.CurrentWeight - _weapon.OneShotWeight < 1)
                {
                    Debug.Log("No weight for shooting!");
                    return false;
                }
                return _lastShotTime + _weapon.ShotDelay < Time.time;
            }
        }

        public bool Shot(Vector3 position, Vector3 direction, LayerMask mask)
        {
            if (!ShotPossible) return false;
            _lastShotTime = Time.time;
            //baseWeightyObject.CurrentWeight -= _weapon.OneShotWeight;
            WeaponController.Instance.MakeShot(_weapon, position, direction, mask);
            return true;
        }
    }
}