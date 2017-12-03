using System.Collections;
using Constrollers;
using UnityEngine;

namespace Core.Field
{
    public class BaseFieldCellObject : MonoBehaviour
    {
        [SerializeField] private FieldCellSettings settings;
        [SerializeField] private int remainingWeight;

        private FieldCellObjectPresentation cellPresentation;
        private Coroutine bouncingCoroutine;
        private Coroutine crashIsComingCoroutine;

        public float WeightDelta { get { return RemainingWeight / (float) settings.maxWeight; } }
        public bool WasCrashed { get; private set; }

        public float Radius
        {
            get { return settings.radius; }
        }

        public int RemainingWeight
        {
            get { return remainingWeight; }
        }

        public int CurrentWeight
        {
            get { return settings.maxWeight - remainingWeight; }
        }

        protected virtual void Awake()
        {
            cellPresentation = GetComponent<FieldCellObjectPresentation>();
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
            remainingWeight = settings.maxWeight;
        }

        public void AddWeight(int weight)
        {
            remainingWeight -= weight;
        }

        public virtual void TestWeight()
        {
            if (WasCrashed) return;
            var position = transform.localPosition;
            var currentWeight = CurrentWeight;
            if (0 == currentWeight)
            {
                if (null == bouncingCoroutine && position.y < 0.0f)
                {
                    bouncingCoroutine = StartCoroutine(Bouncing(position));
                }
                if (crashIsComingCoroutine != null)
                {
                    StopCoroutine(crashIsComingCoroutine);
                    crashIsComingCoroutine = null;
                    if (cellPresentation != null)
                        cellPresentation.StopShake();
                }
                CancelInvoke("CrashCell");
            }
            else 
            {
                position.y = -currentWeight * settings.weightToPositionRation;
                transform.localPosition = position;
                if (bouncingCoroutine != null)
                {
                    StopCoroutine(bouncingCoroutine);
                    bouncingCoroutine = null;
                }
                if (remainingWeight <= 0 &&
                    null == crashIsComingCoroutine)
                {
                    crashIsComingCoroutine = StartCoroutine(CrashIsComing());
                }
            }
        }

        private IEnumerator CrashIsComing()
        {
            if (cellPresentation != null)
                cellPresentation.StartShake();
            yield return new WaitForSeconds(settings.delayBeforeFalling);
            CrashCell();
            crashIsComingCoroutine = null;
            if (cellPresentation != null)
                cellPresentation.StopShake();
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
            for (var t = 0.0f; settings.minYPosition < position.y; t += Time.deltaTime)
            {
                yield return null;
                position.y = y + settings.fallingAcceleration * t * t * 0.5f;
                transform.localPosition = position;
            }
            gameObject.SetActive(false);
        }

        private IEnumerator Bouncing(Vector3 startPosition)
        {
            transform.localPosition = startPosition;
            for (float x = 0, max = -startPosition.y * settings.offsetToAcceleration; x <= max; x += settings.springRate * Time.deltaTime)
            {
                var p = transform.localPosition;
                p.y = -Mathf.Sin(x) * startPosition.y * (1.0f - x / max);
                transform.localPosition = p;
                yield return null;
            }
        }    
    }
}