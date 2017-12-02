using Constrollers.Input;
using UnityEngine;

namespace Core.Movement
{
    class PlayerMovementObject : BaseMovementObject
    {
        protected override Vector3 Direction
        {
            get
            {
                var dir = InputController.Instance.GetMovementDirectin();
                return new Vector3(dir.x, 0, dir.y);
            }
        }
    }
}
