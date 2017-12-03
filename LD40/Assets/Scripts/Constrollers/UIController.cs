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
        [SerializeField] private Text _weightlostText;
        [SerializeField] private Text _weightlostTextShadow;
        [SerializeField] private Text _weightAddedText;
        [SerializeField] private Text _weightTextAddedShadow;
        [SerializeField] private Text _weightText;
        [SerializeField] private Text _weightTextShadow;
        [SerializeField] private StatisticController _statisticController;
        [SerializeField] private Transform _ui;

        public override void AwakeSingleton()
        {

        }

        protected virtual void Update()
        {
            UpdateWeightUI();
        }

        public void HideUI()
        {
            _ui.gameObject.SetActive(false);
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
            _weightlostText.text = string.Format("Weight lost {0}", _statisticController.WeightRemoved);
            _weightlostTextShadow.text = string.Format("Weight lost {0}", _statisticController.WeightRemoved);
            _weightAddedText.text = string.Format("Weight added {0}", _statisticController.WeightAdded);
            _weightTextAddedShadow.text = string.Format("Weight added {0}", _statisticController.WeightAdded);
        }
    }
}
