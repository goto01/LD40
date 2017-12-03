using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Constrollers
{
    class PauseController : BaseController<PauseController>
    {
        [SerializeField] private bool _paused ;
        [SerializeField] private GameObject _pauseUI;
        [SerializeField] private GameObject _restartUI;
        [SerializeField] private int _startSceneId;

        public bool Paused { get { return _paused; } }

        public override void AwakeSingleton()
        {
            _pauseUI.SetActive(false);
        }

        public void Pause()
        {
            _pauseUI.SetActive(true);
            _paused = true;
            Time.timeScale = 0;
        }

        public void Continue()
        {
            _pauseUI.SetActive(false);
            _paused = false;
            Time.timeScale = 1;
        }

        public void ShowRestartUI()
        {
            EffectController.Instance.FadeOut();
            Invoke("Restart", EffectController.Instance.FadeDuration);
            
        }

        private void Restart()
        {
            _paused = true;
            _restartUI.gameObject.SetActive(true);
        }

        public void SwitchToBeginScene()
        {
            SceneManager.LoadScene(_startSceneId);
        }
    }
}
