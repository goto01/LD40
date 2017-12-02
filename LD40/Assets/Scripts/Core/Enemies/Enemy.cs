﻿using System;
﻿using Constrollers;
using Core.Field;
using Core.Movement;
using Core.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        public event Action<Enemy> Destroyed = delegate { };
        
        private BaseWeightyObject weightyObject;
        private WeaponComponent weaponComponent;
        private float nextShootTime;

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
                vector.y = 0.0f;
                return vector;
            }
        }

        private void Awake()
        {
            weightyObject = GetComponent<BaseWeightyObject>();
            weaponComponent = GetComponent<WeaponComponent>();
        }

        public virtual void Update()
        {
            Walk();
            Shoot();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Destroyed.Invoke(this);
        }

        public void Init(Vector3 fieldCenter, Vector2 fieldSize)
        {
            var position = fieldCenter;
            position.x += fieldSize.x * (Random.value - 0.5f);
            position.y = 0.0f;
            position.z += fieldSize.y * (Random.value - 0.5f);
            transform.position = position;
        }

        private void Walk()
        {
            var vector = VectorToPlayer;
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