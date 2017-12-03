using Core;
using Core.Field;
using Core.Weapons;
using UnityEngine;

namespace Constrollers
{
    class PlayerController : BaseController<PlayerController>
    {
        [SerializeField] private Player _player;
        [SerializeField] private LayerMask _playerBulletsLayerMask;

        public LayerMask PlayerBulletsLayerMask { get { return _playerBulletsLayerMask; } }

        public BaseWeightyObject PLayerWeightyObject { get { return Player.GetComponent<BaseWeightyObject>(); } }

        public Player Player
        {
            get { return _player; }
        }

        public override void AwakeSingleton()
        {
            
        }

        protected virtual void Update()
        {
            if (PauseController.Instance.Paused) return;
            if (!_player.gameObject.activeSelf) PauseController.Instance.ShowRestartUI();
            _player.UpdateSelf();
        }
    }
}
    