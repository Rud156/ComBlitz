using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComBlitz.Scene
{
    public class PlayAndQuit : MonoBehaviour
    {
        public void PlayGame()
        {
            NextSceneData.sceneToLoad = 2;
            SceneManager.LoadScene(1);
        }

        public void QuitGame() => Application.Quit();
    }
}