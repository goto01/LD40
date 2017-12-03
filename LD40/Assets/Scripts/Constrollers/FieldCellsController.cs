using System.Collections.Generic;
using Core.Field;
using UnityEngine;

namespace Constrollers
{
    class FieldCellsController : BaseController<FieldCellsController>
    {
        [SerializeField] private List<BaseFieldCellObject> cellObjects;
        [SerializeField] private List<BaseWeightyObject> weightyObjects;

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
            for (int i = weightyObjects.Count - 1; 0 <= i; --i)
            {
                var weightyObject = weightyObjects[i];
                if (weightyObject.WasFall) continue;
                var position = weightyObject.transform.position;
                var cellObject = GetCell(position);
                if (cellObject != null && !cellObject.WasCrashed)
                {
                    weightyObject.SetViewOffset(cellObject.GetComponent<FieldCellObjectPresentation>().ViewOffset);
                    cellObject.AddWeight(weightyObject.CurrentWeight);
                    position = weightyObject.transform.localPosition;
                    position.y = cellObject.transform.localPosition.y;
                    weightyObject.transform.localPosition = position;
                    weightyObject.ResetFalling();
                }
                else
                {
                    weightyObject.SetViewOffset(Vector3.zero);
                    weightyObject.PrepareToFall();
                }
            }
            for (int i = cellObjects.Count - 1; 0 <= i; --i)
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