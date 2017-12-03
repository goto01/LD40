using System.Collections;
using Constrollers;
using Core.Movement;
using UnityEngine;

namespace Core.Field
{
    public class BaseWeightyObject : MonoBehaviour
    {
        [SerializeField] private int minWeight = 1;
        [SerializeField] private int startWeight = 5;
        [SerializeField] private int currentWeight;
        [SerializeField] private float weightToScaleRate = 1.0f;
        [SerializeField] private float distanceToWeightRate = 1.0f;
        [Header("Falling")]
        [SerializeField] private bool _notFallable;
        [SerializeField] private float fallingDelay = 1.0f;
        [SerializeField] private float fallingAcceleration = -20.0f;
        [SerializeField] private float minYPosition = -30.0f;
        
        public bool NotFallable
        {
            get { return _notFallable; }
            set { _notFallable = true; }
        }
        
        private float? fallingTime;
        private float additionalWeight;

        public bool WasFall { get; private set; }

        public int CurrentWeight
        {
            get { return currentWeight; }
            set
            {
                if (currentWeight == value) return;
                currentWeight = value;
                OnWeightWasChanged();
            }
        }

        public void Reset()
        {
            fallingTime = null;
            WasFall = false;
            CurrentWeight = startWeight;
            var movementObject = GetComponent<BaseMovementObject>();
            if (movementObject != null)
                movementObject.enabled = true;
        }

        public void OnMoved(Vector3 delta)
        {
            delta.y = 0.0f;
            additionalWeight = CurrentWeight + additionalWeight;
            additionalWeight -= distanceToWeightRate * delta.magnitude;
            CurrentWeight = Mathf.Max(Mathf.CeilToInt(additionalWeight), minWeight);
            additionalWeight -= CurrentWeight;
            additionalWeight = Mathf.Max(additionalWeight, 0.0f);
        }

        protected virtual void OnEnable()
        {
            Reset();
            FieldCellsController.Instance.Register(this);
        }

        protected virtual void OnDisable()
        {
            if (!FieldCellsController.WasDestoyed)
                FieldCellsController.Instance.Remove(this);
        }

        public void PrepareToFall()
        {
            if (_notFallable) return;
            if (WasFall) return;
            var time = Time.time;
            if (fallingTime < time)
            {
                WasFall = true;
                StartCoroutine(Falling());
                OnWasFall();
            }
            else if (!fallingTime.HasValue)
            {
                fallingTime = time + fallingDelay;
            }
        }

        public void ResetFalling()
        {
            fallingTime = null;
        }

        private void OnWeightWasChanged()
        {
            if (currentWeight < minWeight)
            {
                currentWeight = minWeight;
                DeathFromExhaustion();
            }
            var scale = currentWeight / (float) startWeight;
            scale = 1.0f + (scale - 1.0f) * weightToScaleRate;
            transform.localScale = Vector3.one * scale;
        }

        private void DeathFromExhaustion()
        {
            Debug.LogFormat("{0} was dead from exhaustion", name);
            gameObject.SetActive(false);
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

        private void OnWasFall()
        {
            FieldCellsController.Instance.Remove(this);
            var movementObject = GetComponent<BaseMovementObject>();
            if (movementObject != null)
                movementObject.enabled = true;
        }
    }
}