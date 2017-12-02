using System.Collections;
using Constrollers;
using UnityEngine;

namespace Core.Field
{
    public class BaseFieldCellObject : MonoBehaviour
    {
        [SerializeField] private float radius = 0.5f;
        [Tooltip("What weight can be sustained?")]
        [SerializeField] private float maxWeight = 5;
        [SerializeField] private float currentWeight;
        [Tooltip("Settings")]
        [SerializeField] private float delayBeforeDestroy = 1.0f;
        [SerializeField] private float weightToPositionRation = 1.0f;
        [SerializeField] private float fallingDuration = 5.0f;

        private bool wasCrashed;

        public float WeightDelta { get { return CurrentWeight/maxWeight; } }

        public float Radius
        {
            get { return radius; }
        }

        public float CurrentWeight
        {
            get { return currentWeight; }
        }

        protected virtual void Awake()
        {
            FieldCellsController.Instance.Register(this);
        }

        protected virtual void OnDestroy()
        {
            if (!FieldCellsController.WasDestoyed)
                FieldCellsController.Instance.Remove(this);
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
            var position = transform.localPosition;
            position.y = (currentWeight - maxWeight) * weightToPositionRation;
            transform.localPosition = position;
            if (currentWeight <= 0)
                Invoke("DestroyCell", delayBeforeDestroy);
            else
                CancelInvoke("DestroyCell");
        }

        private void DestroyCell()
        {
            if (wasCrashed) return;
            wasCrashed = true;
            StartCoroutine(Falling());
        }

        IEnumerator Falling()
        {
            var acceleration = 1.0f;
            var speed = 0.0f;
            for (float t = 0.0f; t < fallingDuration; t += Time.deltaTime)
            {
                var dt = Time.deltaTime;
                var position = transform.position;
                position.y -= speed * dt + acceleration * dt * dt * 0.5f;
                transform.position = position;
                speed += speed + acceleration * dt;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}