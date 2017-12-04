using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CutsceneController : MonoBehaviour
{
    public int nextSceneId = 2;
    public Image image;
    public Color endColor;
    public Sprite[] slides;

    int index;
    
    private string FadeInParameter = "FadeIn";
    private string FadeOutParameter = "FadeOut";

    private void Start()
    {
        EffectController.Instance.FadeOut();
        AudioController.Play("Cutscene");
    }

    public void NextSlide()
    {
        ++index;
        if (index < slides.Length)
        {
            image.sprite = slides[index];
            AudioController.Play("Skip");
        }
        else
            NextScene();
    }

    public void NextScene()
    {
        image.sprite = null;
        image.color = endColor;
        EffectController.Instance.FadeOut();
        Invoke("SwitchToNextScene", EffectController.Instance.FadeDuration);
    }

    private void SwitchToNextScene()
    {
        SceneManager.LoadScene(nextSceneId);
    }
}
