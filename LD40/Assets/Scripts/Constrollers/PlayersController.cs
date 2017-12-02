using Core;
using UnityEngine;

namespace Constrollers
{
    class PlayerController : BaseController<PlayerController>
    {
        [SerializeField] private Player _player;

        public override void AwakeSingleton()
        {
            
        }
    }
}
    