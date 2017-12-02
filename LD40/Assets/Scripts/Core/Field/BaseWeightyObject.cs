using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Constrollers;
using UnityEngine;

namespace Assets.Scripts.Core.Field
{
    public class BaseWeightyObject : MonoBehaviour
    {
        [SerializeField] private int currentWeight = 5;

        public int CurrentWeight
        {
            get { return currentWeight; }
            set { currentWeight = value; }
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
    }
}