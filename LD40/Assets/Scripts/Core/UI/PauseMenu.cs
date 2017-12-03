using System.Collections;
using Constrollers;
using Constrollers.Input;
using Controllers;
using UnityEngine;

namespace Core.UI
{
    class PauseMenu : MonoBehaviour
    {
        protected virtual void Start()
        {
            EffectController.Instance.FadeIn();
        }

        protected virtual void Update()
        {
            if (!InputController.Instance.GetPauseButtonDown()) return;
            if (PauseController.Instance.Paused) PauseController.Instance.Continue();
            else PauseController.Instance.Pause();
        }
    }
}
