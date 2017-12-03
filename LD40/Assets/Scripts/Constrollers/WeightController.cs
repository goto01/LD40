using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controllers;
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
            @object.CurrentWeight += weight;
            EffectController.Instance.SpawnPlus(@object.transform.position);
        }
    }
}
