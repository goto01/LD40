using UnityEngine;

namespace Constrollers.InputController
{
    public abstract class InputController : BaseController<InputController>
    {
        public abstract Vector2 GetMovementDirectin();
    }
}
