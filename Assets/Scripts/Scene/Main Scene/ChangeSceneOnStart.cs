using System.Collections;
using ComBlitz.Extensions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ComBlitz.Scene.Data;

namespace ComBlitz.Scene.MainScene
{
    public class ChangeSceneOnStart : MonoBehaviour
    {
        public GameObject infoText;
        public Slider loadingSlider;
        public float bufferTime = 2f;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            if (SceneData.showInfo)
            {
                infoText.SetActive(true);
                infoText.GetComponent<Text>().text =
                    $"Killed {SceneData.sceneKills} in {ExtensionFunctions.Format2DecimalPlace(SceneData.sceneTime)} s";
            }
            else
            {
                infoText.SetActive(false);
                infoText.GetComponent<Text>().text = "";
            }

            StartCoroutine(LoadNextSceneAsync());
        }

        IEnumerator LoadNextSceneAsync()
        {
            loadingSlider.value = 0;
            if (SceneData.showInfo)
                yield return new WaitForSeconds(bufferTime);

            int sceneIndex = SceneData.sceneToLoad;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                loadingSlider.value = operation.progress;
                yield return null;
            }
        }
    }
}