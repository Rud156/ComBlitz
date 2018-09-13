using System.Collections;
using System.Collections.Generic;
using ComBlitz.Extensions;
using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComBlitz.StoryScene
{
    public class StorySceneController : MonoBehaviour
    {
        [Header("Dialogue")] public Text dialogueText;
        [TextArea] public string[] dialogues;
        public float waitTimeBewteenDialogues;

        [Header("Fade Texture")] public RawImage fadeImage;
        public float fadeRate;

        private bool startFading;
        private float currentAlpha;

        private void Start()
        {
            startFading = false;
            fadeImage.color = new Color(0, 0, 0, 0);
            currentAlpha = 0;

            StartCoroutine(DisplayDialogues());
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

        private IEnumerator DisplayDialogues()
        {
            foreach (string dialogue in dialogues)
            {
                dialogueText.text = dialogue;
                yield return new WaitForSeconds(waitTimeBewteenDialogues);
            }
        }
    }
}