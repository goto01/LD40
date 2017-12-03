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
        [SerializeField] private string _dashButtonInputName;
        [SerializeField] private string _pauseButtonInputName;
        private Camera _camera;

        private int LeftInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_leftInputName)); } }
        private int RightInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_rightInputName)); } }
        private int UpInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_upInputName)); } }
        private int DownInputValue { get { return Mathf.CeilToInt(UnityEngine.Input.GetAxis(_downInputName)); } }

        public override Vector2 GetMovementDirection()
        {
            var inputVector = new Vector3(RightInputValue - LeftInputValue, 0.0f, UpInputValue - DownInputValue);
            var cameraVector = -Camera.main.transform.position;
            cameraVector.y = 0.0f;
            var rotation = Quaternion.FromToRotation(Vector3.forward, cameraVector.normalized);
            inputVector = rotation * inputVector;
            return new Vector2(inputVector.x, inputVector.z);
        }

        public override bool GetShotButtonPressed()
        {
            return UnityEngine.Input.GetButton(_shotButtonInputName);
        }

        public override bool GetDashButtonDown()
        {
            return UnityEngine.Input.GetButtonDown(_dashButtonInputName);
        }

        public override Vector3 GetPointerDirectionFrom(Vector3 point)
        {
            var plane = new Plane(Vector3.up, Vector3.zero);
            var ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            float enter;
            plane.Raycast(ray, out enter);
            return (ray.GetPoint(enter) - point).normalized;
        }

        public override bool GetPauseButtonDown()
        {
            return UnityEngine.Input.GetButtonDown(_pauseButtonInputName);
        }

        public override void AwakeSingleton()
        {
            _camera = Camera.main;
        }
    }
}
