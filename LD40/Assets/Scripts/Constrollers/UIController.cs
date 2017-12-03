using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Constrollers
{
    class UIController : BaseController<UIController>
    {
        [SerializeField] private float _maxWeightValue;
        [SerializeField] private Image _weightBar;
        [SerializeField] private Image _weightBarShadow;
        [SerializeField] private Image _distanceBar;
        [SerializeField] private Image _distanceBarShadow;
        [SerializeField] private Text _weightText;
        [SerializeField] private Text _weightTextShadow;

        public override void AwakeSingleton()
        {

        }

        protected virtual void Update()
        {
            UpdateWeightUI();
        }

        private void UpdateWeightUI()
        {
            var weightlyObject = PlayerController.Instance.PLayerWeightyObject;
            var value = Mathf.Clamp01(weightlyObject.CurrentWeight/_maxWeightValue);
            _weightBar.material.SetFloat("_ProgressValue", value);
            _weightBarShadow.material.SetFloat("_ProgressValue", value);
            _weightText.text = string.Format("Weight {0}", weightlyObject.CurrentWeight);
            _weightTextShadow.text = string.Format("Weight {0}", weightlyObject.CurrentWeight);
           _distanceBar.material.SetFloat("_ProgressValue", 1 - weightlyObject.AdditionaWeight);
           _distanceBarShadow.material.SetFloat("_ProgressValue", 1 - weightlyObject.AdditionaWeight);
        }
    }
}
