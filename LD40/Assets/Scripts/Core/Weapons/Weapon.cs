using System;
using UnityEngine;

namespace Core.Weapons
{
    [Serializable]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private float _shotDelay;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _angleRange;
        [SerializeField] private int _bulletsPerShot;
        [SerializeField] private int _weightValue;

        public float ShotDelay { get { return _shotDelay; } }
        public float BulletSpeed { get { return _bulletSpeed; } }
        public float AngleRange { get { return _angleRange; } }
        public float RandomAngleRange { get { return UnityEngine.Random.Range(-_angleRange, _angleRange); } }
        public int BulletsPerShot { get { return _bulletsPerShot; } }
        public int WeightValue { get { return _weightValue; } }
    }
}
