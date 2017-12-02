using Constrollers;
using UnityEngine;

namespace Core.Field
{
    public class BaseWeightyObject : MonoBehaviour
    {
        [SerializeField] private int startWeight = 5;
        [SerializeField] private int currentWeight;
        [SerializeField] private float weightToScaleRate = 1.0f;

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

        protected virtual void OnEnable()
        {
            CurrentWeight = startWeight;
            FieldCellsController.Instance.Register(this);
        }

        protected virtual void OnDisable()
        {
            if (!FieldCellsController.WasDestoyed)
                FieldCellsController.Instance.Remove(this);
        }

        private void OnWeightWasChanged()
        {
            var scale = currentWeight * weightToScaleRate / startWeight;
            transform.localScale = Vector3.one * scale;
        }
    }
}