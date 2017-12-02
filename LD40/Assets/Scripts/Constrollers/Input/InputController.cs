using UnityEngine;

namespace Constrollers.Input
{
    public abstract class InputController : BaseController<InputController>
    {
        public abstract Vector2 GetMovementDirectin();

        public abstract bool GetShotButtonDown();

        public abstract Vector3 GetPointerDirectionFrom(Vector3 point);
    }
}
