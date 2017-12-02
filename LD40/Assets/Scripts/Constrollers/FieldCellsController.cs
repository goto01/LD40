using System.Collections.Generic;
using Assets.Scripts.Core.Field;
using Constrollers;
using UnityEngine;

namespace Assets.Scripts.Constrollers
{
    class FieldCellsController : BaseController<FieldCellsController>
    {
        [SerializeField] private List<BaseFieldCellObject> objects;
        [SerializeField] private float testWeightInterval = 1.0f;

        private float testWeightTimeRemain;

        public override void AwakeSingleton()
        {
        }

        public void Register(BaseFieldCellObject cellObject)
        {
            objects.Add(cellObject);
        }

        public void Remove(BaseFieldCellObject cellObject)
        {
            objects.Remove(cellObject);
        }

        protected virtual void Update()
        {
            testWeightTimeRemain -= Time.deltaTime;
            if (testWeightTimeRemain < 0)
            {
                TestWeight();
                testWeightTimeRemain = testWeightInterval;
            }
        }

        private void TestWeight()
        {
            for (int i = 0, n = objects.Count; i < n; ++i)
            {
                objects[i].TestWeight();
            }
        }
    }
}