using ComBlitz.Extensions;
using ComBlitz.Scene.Data;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComBlitz.StoryScene
{
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

        [Header("Display")] public RawImage fadeImage;
        public float fadeRate;

        private bool startFading;
        private float currentAlpha;

        private void Start()
        {
            startFading = false;
            fadeImage.color = new Color(0, 0, 0, 0);
            currentAlpha = 0;
        }

        private void Update()
        {
            if (!startFading)
                return;

            currentAlpha += Time.deltaTime * fadeRate;
            fadeImage.color = ExtensionFunctions.ConvertAndClampColor(0, 0, 0, currentAlpha);

            if (!(currentAlpha >= 255))
                return;

            SceneData.sceneToLoad = 3;
            SceneManager.LoadScene(1);
        }

        public bool ActivateFading() => startFading = true;
    }
}