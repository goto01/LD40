﻿using System.Collections;
using Constrollers;
using UnityEngine;

namespace Core.Field
{
    public class BaseFieldCellObject : MonoBehaviour
    {
        [SerializeField] private float radius = 0.5f;
        [Header("What weight can be sustained?")]
        [SerializeField] private float maxWeight = 5;
        [SerializeField] private float currentWeight;
        [Header("Settings")]
        [SerializeField] private float delayBeforeDestroy = 1.0f;
        [SerializeField] private float weightToPositionRation = 1.0f;
        [Header("Falling")]
        [SerializeField] private float fallingAcceleration = -20.0f;
        [SerializeField] private float minYPosition = -30.0f;
        [Header("Bouncing")]
        [SerializeField] private float offsetToAcceleration = 1.0f;
        [SerializeField] private float springRate = 1.0f;
        

        private Coroutine BouncingCoroutine;

        public float WeightDelta { get { return CurrentWeight/maxWeight; } }
        public bool WasCrashed { get; private set; }

        public float Radius
        {
            get { return radius; }
        }

        public float CurrentWeight
        {
            get { return currentWeight; }
        }

        protected virtual void OnEnable()
        {
            FieldCellsController.Instance.Register(this);
        }

        protected virtual void OnDisable()
        {
            if (!FieldCellsController.WasDestoyed)
                FieldCellsController.Instance.Remove(this);
        }

        public void ResetWeight()
        {
            currentWeight = maxWeight;
        }

        public void AddWeight(float weight)
        {
            currentWeight -= weight;
        }

        public virtual void TestWeight()
        {
            if (WasCrashed) return;
            var position = transform.localPosition;
            if (currentWeight <= 0)
            {
                position.y = (currentWeight - maxWeight) * weightToPositionRation;
                transform.localPosition = position;
                if (BouncingCoroutine != null)
                {
                    StopCoroutine(BouncingCoroutine);
                    BouncingCoroutine = null;
                }
                Invoke("CrashCell", delayBeforeDestroy);
            }
            else
            {
                if (null == BouncingCoroutine && position.y < 0.0f)
                {
                    BouncingCoroutine = StartCoroutine(Bouncing(position));
                }
                CancelInvoke("CrashCell");
            }
        }

        private void CrashCell()
        {
            if (WasCrashed) return;
            WasCrashed = true;
            StartCoroutine(Falling());
        }

        private IEnumerator Falling()
        {
            var position = transform.localPosition;
            var y = position.y;
            for (var t = 0.0f; minYPosition < position.y; t += Time.deltaTime)
            {
                yield return null;
                position.y = y + fallingAcceleration * t * t * 0.5f;
                transform.localPosition = position;
            }
            gameObject.SetActive(false);
        }

        private IEnumerator Bouncing(Vector3 startPosition)
        {
            transform.localPosition = startPosition;
            for (float x = 0, max = -startPosition.y * offsetToAcceleration; x <= max; x += springRate * Time.deltaTime)
            {
                var p = transform.localPosition;
                p.y = -Mathf.Sin(x) * startPosition.y * (1.0f - x / max);
                transform.localPosition = p;
                yield return null;
            }
        }    
    }
}