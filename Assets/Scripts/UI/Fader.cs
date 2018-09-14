using ComBlitz.Extensions;
using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComBlitz.UI
{
    [RequireComponent(typeof(Image))]
    public class Fader : MonoBehaviour
    {
        #region Singleton

        public static Fader instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public delegate void FadeInComplete();

        public delegate void FadeOutComplete();

        public FadeInComplete fadeInComplete;
        public FadeOutComplete fadeOutComplete;

        [Header("Fade Rate")] public float fadeInRate;
        public float fadeOutRate;

        private Image fadeImage;
        private bool activateFadeIn;
        private bool activateFadeOut;
        private float currentAlpha;

        private void Start() => fadeImage = GetComponent<Image>();

        private void Update()
        {
            if (activateFadeIn)
                FadeIn();
            else if (activateFadeOut)
                FadeOut();
        }

        public void StartFadeIn()
        {
            currentAlpha = 255;
            fadeImage.color = Color.black;

            activateFadeIn = true;
            activateFadeOut = false;
        }

        private void FadeIn()
        {
            currentAlpha -= fadeInRate * Time.deltaTime;
            fadeImage.color = ExtensionFunctions.ConvertAndClampColor(0, 0, 0, currentAlpha);

            if (!(currentAlpha <= 0))
                return;

            fadeInComplete?.Invoke();
            activateFadeIn = false;
        }

        public void StartFadeOut()
        {
            currentAlpha = 0;
            fadeImage.color = new Color(0, 0, 0, 0);

            activateFadeOut = true;
            activateFadeIn = false;
        }

        private void FadeOut()
        {
            currentAlpha += fadeOutRate * Time.deltaTime;
            fadeImage.color = ExtensionFunctions.ConvertAndClampColor(0, 0, 0, currentAlpha);

            if (!(currentAlpha >= 255))
                return;

            fadeOutComplete?.Invoke();
            activateFadeOut = false;
        }
    }
}