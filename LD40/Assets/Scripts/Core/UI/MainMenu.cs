using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.UI
{
    class MainMenu : MonoBehaviour
    {
        [SerializeField] private int _sceneId;
        [SerializeField] private Animator _menuAnimator;

        private string FadeInParameter = "FadeIn";
        private string FadeOutParameter = "FadeOut";

        protected virtual void Start()
        {
            EffectController.Instance.FadeOut();
            _menuAnimator.SetTrigger(FadeInParameter);
        }

        public void SwitchScene()
        {
            EffectController.Instance.FadeIn();
            _menuAnimator.SetTrigger(FadeOutParameter);
            Invoke("SwitchToNextScene", EffectController.Instance.FadeDuration);
        }

        private void SwitchToNextScene()
        {
            SceneManager.LoadScene(_sceneId);
        }
    }
}
