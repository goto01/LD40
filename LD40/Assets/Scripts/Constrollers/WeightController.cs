using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Enemies;
using Core.Field;
using UnityEngine;

namespace Constrollers
{
    class WeightController : BaseController<WeightController>
    {
        public override void AwakeSingleton()
        {
            
        }

        public void GiveWeightToObject(int weight, BaseWeightyObject @object)
        {
            @object.AddWeight(weight);
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
