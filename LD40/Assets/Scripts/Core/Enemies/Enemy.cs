﻿using System;
﻿using System.Collections;
﻿using Constrollers;
﻿using Controllers;
﻿using Core.Field;
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
        [SerializeField] private bool _spawning;
        [SerializeField] private float _spawnDuration;
        [SerializeField] private float _spawnHeight;

        public bool Spawning { get { return _spawning; } }

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

        public override void UpdateSelf()
        {
            if (_spawning) return;
            base.UpdateSelf();
        }

        public virtual void Update()
        {
            if (_spawning) return;
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
            transform.position = position + Vector3.up*_spawnHeight;
            StartCoroutine(Spawn(position));
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

        private IEnumerator Spawn(Vector3 origin)
        {
            _spawning = true;
            var startTime = Time.time;
            transform.position = origin +  Vector3.up* _spawnHeight;
            while (startTime + _spawnDuration > Time.time)
            {
                //_shadow.position = Vector3.zero;
                var pos = transform.position;
                pos.y = Mathf.Lerp(_spawnHeight, origin.y, (Time.time - startTime)/_spawnDuration);
                transform.position = pos;
                yield return null;
            }
            transform.position = origin;
            _spawning = false;
            EffectController.Instance.Shake();
        }
    }
}