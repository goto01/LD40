using System.Collections.Generic;
using Core.Movement;
using UnityEngine;

namespace Constrollers
{
    class MovementController : BaseController<MovementController>
    {
        [SerializeField] private List<BaseMovementObject> _movementObjects; 
        
        public override void AwakeSingleton()
        {
            
        }

        public void Register(BaseMovementObject movementObject)
        {
            _movementObjects.Add(movementObject);
        }

        public void Remove(BaseMovementObject movementObject)
        {
            _movementObjects.Remove(movementObject);
        }

        protected virtual void Update()
        {
            if (PauseController.Instance.Paused) return;
            UpdateMovementObjects();
        }

        private void UpdateMovementObjects()
        {
            for (var index = 0; index < _movementObjects.Count; index++) _movementObjects[index].UpdateSelf();
        }
    }
}
