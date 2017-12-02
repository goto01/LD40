using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Enemies;
using Core.Field;

namespace Constrollers
{
    class WeightController : BaseController<WeightController>
    {
        public override void AwakeSingleton()
        {
            
        }

        public void GiveWeightToObject(int weight, BaseWeightyObject enemy)
        {
            enemy.CurrentWeight += weight;
        }

        public void TakeWeightFromPlayer(int weight)
        {
            PlayerController.Instance.Player.GetComponent<BaseWeightyObject>().CurrentWeight -= weight;
        }

        public void GiveWeightToPlayer(int weight)
        {
            PlayerController.Instance.Player.GetComponent<BaseWeightyObject>().CurrentWeight += weight;
        }
    }
}
