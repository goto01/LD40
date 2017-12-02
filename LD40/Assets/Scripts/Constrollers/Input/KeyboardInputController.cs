using UnityEngine;

namespace Constrollers.Input
{
    class KeyboardInputController : InputController
    {
        [SerializeField] private string _leftInputName;
        [SerializeField] private string _rightInputName;
        [SerializeField] private string _upInputName;
        [SerializeField] private string _downInputName;
        [SerializeField] private string _shotButtonInputName;
        private Camera _camera;

        private int LeftInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_leftInputName)); } }
        private int RightInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_rightInputName)); } }
        private int UpInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_upInputName)); } }
        private int DownInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_downInputName)); } }

        public override Vector2 GetMovementDirectin()
        {
            return new Vector2(RightInputValue - LeftInputValue, UpInputValue - DownInputValue);
        }

        public override bool GetShotButtonPressed()
        {
            return UnityEngine.Input.GetButton(_shotButtonInputName);
        }
        
        public override Vector3 GetPointerDirectionFrom(Vector3 point)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            float enter;
            plane.Raycast(ray, out enter);
            return (ray.GetPoint(enter) - point).normalized;
        }

        public override void AwakeSingleton()
        {
            _camera = Camera.main;
        }
    }
}
