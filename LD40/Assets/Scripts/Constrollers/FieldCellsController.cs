using System.Collections.Generic;
using Core.Field;
using UnityEngine;

namespace Constrollers
{
    class FieldCellsController : BaseController<FieldCellsController>
    {
        [SerializeField] private List<BaseFieldCellObject> cellObjects;
        [SerializeField] private List<BaseWeightyObject> weightyObjects;
        [SerializeField] private float testWeightInterval = 1.0f;

        private float testWeightTimeRemain;

        public override void AwakeSingleton()
        {
        }

        public void Register(BaseWeightyObject weightyObject)
        {
            weightyObjects.Add(weightyObject);
        }

        public void Remove(BaseWeightyObject weightyObject)
        {
            weightyObjects.Remove(weightyObject);
        }

        public void Register(BaseFieldCellObject cellObject)
        {
            cellObjects.Add(cellObject);
        }

        public void Remove(BaseFieldCellObject cellObject)
        {
            cellObjects.Remove(cellObject);
        }

        protected virtual void Update()
        {
            // todo: optimize this
            for (int i = 0, n = cellObjects.Count; i < n; ++i)
            {
                cellObjects[i].ResetWeight();
            }
            for (int i = 0, n = weightyObjects.Count; i < n; ++i)
            {
                var weightyObject = weightyObjects[i];
                var position = weightyObject.transform.position;
                var cellObject = GetCell(position);
                if (cellObject != null)
                    cellObject.AddWeight(weightyObject.CurrentWeight);
            }
            for (int i = 0, n = cellObjects.Count; i < n; ++i)
            {
//                Debug.LogFormat("cellObjects[{0}]={1}", i, cellObjects[i].CurrentWeight);
                cellObjects[i].TestWeight();
            }
        }

        private BaseFieldCellObject GetCell(Vector3 position)
        {
            for (int i = 0, n = cellObjects.Count; i < n; ++i)
            {
                var cellObject = cellObjects[i];
                var p = cellObject.transform.position;
                if (p.x - cellObject.Radius <= position.x && position.x < p.x + cellObject.Radius &&
                    p.z - cellObject.Radius <= position.z && position.z < p.z + cellObject.Radius)
                {
                    return cellObject;
                }
            }
            return null;
        }
    }
}