using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ComBlitz.Scene.Data;

namespace ComBlitz.Scene
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
            if (NextSceneData.showInfo)
            {
                infoText.SetActive(true);
                infoText.GetComponent<Text>().text =
                    $"Killed {NextSceneData.sceneKills} in {NextSceneData.sceneTime} s";
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
            yield return new WaitForSeconds(bufferTime);

            int sceneIndex = NextSceneData.sceneToLoad;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                loadingSlider.value = operation.progress;
                yield return null;
            }
        }
    }
}