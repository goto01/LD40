using Constrollers;
using UnityEngine;

namespace Core.Field
{
    public class BaseWeightyObject : MonoBehaviour
    {
        [SerializeField] private int currentWeight = 5;

        public int CurrentWeight
        {
            get { return currentWeight; }
            set { currentWeight = value; }
        }

        public void AddWeight(int weight)
        {
            currentWeight += weight;
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
    }
}