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
        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            pauseMenu.SetActive(false);
        }

        /// <summary>
        /// Callback sent to all game objects when the player pauses.
        /// </summary>
        /// <param name="pauseStatus">The pause state of the application.</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                PauseGame();
        }

        public void PauseGame()
        {
            pauseMenu.SetActive(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }

        public void GoToMainMenu()
        {
            NextSceneData.sceneToLoad = 0;
            SceneManager.LoadScene(1);
        }
    }
}