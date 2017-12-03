using Constrollers.Input;
using UnityEngine;

namespace Core.Movement
{
    class PlayerMovementObject : BaseMovementObject
    {
        [SerializeField] private bool _blockGettingDirection;
        [SerializeField] private Vector3 _direction;

        public bool BlockGettingDirection
        {
            get { return _blockGettingDirection; }
            set
            {
                if (Mathf.Abs(_direction.magnitude) < Mathf.Epsilon) _direction = Vector3.forward;
                _blockGettingDirection = value;
            }
        }

        protected override Vector3 Direction
        {
            get
            {
                if (_blockGettingDirection) return _direction;
                var dir = InputController.Instance.GetMovementDirection();
                _direction = new Vector3(dir.x, 0, dir.y);
                return _direction;
            }
        }
    }
}
