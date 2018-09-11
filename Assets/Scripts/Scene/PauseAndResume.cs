using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComBlitz.Scene
{
    public class PauseAndResume : MonoBehaviour
    {
        #region Singleton

        public static PauseAndResume instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public GameObject pauseMenu;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update
        /// methods is called the first time.
        /// </summary>
        private void Start() => pauseMenu.SetActive(false);

        public void PauseGame()
        {
            pauseMenu.SetActive(true);

            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);

            Time.timeScale = 1;
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1;
            NextSceneData.sceneToLoad = 0;
            SceneManager.LoadScene(1);
        }
    }
}