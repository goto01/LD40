using System.Collections;
using Constrollers;
using Core.Enemies;
using Core.Movement;
using UnityEngine;

namespace Core.Field
{
    public class BaseWeightyObject : MonoBehaviour
    {
        [SerializeField] private int startWeight = 5;
        [SerializeField] private int currentWeight;
        [SerializeField] private float weightToScaleRate = 1.0f;
        [Header("Falling")]
        [SerializeField] private float fallingDelay = 1.0f;
        [SerializeField] private float fallingAcceleration = -20.0f;
        [SerializeField] private float minYPosition = -30.0f;
        
        private float? fallingTime;
        
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
            var scale = currentWeight / (float)startWeight;
            scale = 1.0f + (scale - 1.0f) * weightToScaleRate;  
            transform.localScale = Vector3.one * scale;
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