using Constrollers;
using Core.Field;
using Core.Movement;
using Core.Weapons;
using UnityEngine;

namespace Core.Enemies
{
    [RequireComponent(typeof(BaseWeightyObject),
                      typeof(WeaponComponent))]
    public class Enemy : BaseMovementObject
    {
        [Header("Enemy")]
        [SerializeField] private float shootInterval = 1.0f;
        [SerializeField] private float minDistanceToPlayer = 2.0f;
        [SerializeField] private float walkbackDistanceToPlayer = 1.0f;
        [SerializeField] private float speedWithoutWeight = 1.0f;
        [SerializeField] private float weightToSpeedRatio = 1.0f;
        [SerializeField] private LayerMask bulletsLayerMask;
        
        private BaseWeightyObject weightyObject;
        private WeaponComponent weaponComponent;
        private float nextShootTime;

        private void Awake()
        {
            weightyObject = GetComponent<BaseWeightyObject>();
            weaponComponent = GetComponent<WeaponComponent>();
        }

        protected override Vector3 Direction
        {
            get
            {
                var vector = VectorToPlayer;
                if (vector == Vector3.zero)
                    vector = new Vector3(Random.value, 0.0f, Random.value);
                return vector.normalized;
            }
        }

        protected Vector3 VectorToPlayer
        {
            get
            {
                var player = PlayerController.Instance.Player;
                var vector = player.transform.position - transform.position;
                return vector;
            }
        }

        public virtual void Update()
        {
            Walk();
            Shoot();
        }

        private void Walk()
        {
            var player = PlayerController.Instance.Player;
            var vector = player.transform.position - transform.position;
            if (minDistanceToPlayer < vector.magnitude)
                _speed = speedWithoutWeight / (weightyObject.CurrentWeight * weightToSpeedRatio);
            else if (vector.magnitude <= walkbackDistanceToPlayer)
                _speed = -speedWithoutWeight / (weightyObject.CurrentWeight * weightToSpeedRatio);
            else
                _speed = 0.0f;
        }

        private void Shoot()
        {
            var wantToShoot = nextShootTime <= Time.time;
            if (wantToShoot)
                MakeShot();
        }

        private void MakeShot()
        {
            weaponComponent.Shot(transform.position, VectorToPlayer.normalized, bulletsLayerMask);
            nextShootTime = Time.time + shootInterval;
        }
    }
}