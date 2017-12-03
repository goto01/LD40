using UnityEngine;

namespace Constrollers.Input
{
    public abstract class InputController : BaseController<InputController>
    {
        public abstract Vector2 GetMovementDirection();

        public abstract bool GetShotButtonPressed();

        public abstract bool GetDashButtonDown();

        public abstract Vector3 GetPointerDirectionFrom(Vector3 point);
    }
}
