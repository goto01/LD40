using Core;
using Core.Weapons;
using UnityEngine;

namespace Constrollers
{
    class PlayerController : BaseController<PlayerController>
    {
        [SerializeField] private Player _player;
        [SerializeField] private LayerMask _playerBulletsLayerMask;

        public LayerMask PlayerBulletsLayerMask { get { return _playerBulletsLayerMask; } }

        public override void AwakeSingleton()
        {
            
        }

        protected virtual void Update()
        {
            _player.UpdateSelf();
        }
    }
}
    