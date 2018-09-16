using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComBlitz.Scene.HomeScene
{
    public class MenuManager : MonoBehaviour
    {
        public Texture2D cursorTexture;
        public GameObject homeMenu;

        [Header("Help Menu")] public GameObject helpMenu;
        public GameObject controlsMenu;
        public GameObject inventoryMenu;

        private void Start() => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseHelpMenu();
        }

        public void OpenHelpMenu()
        {
            homeMenu.SetActive(false);
            helpMenu.SetActive(true);
        }

        public void NextButtonPressed()
        {
            controlsMenu.SetActive(false);
            inventoryMenu.SetActive(true);
        }

        public void CloseHelpMenu()
        {
            homeMenu.SetActive(true);
            helpMenu.SetActive(false);
            controlsMenu.SetActive(true);
            inventoryMenu.SetActive(false);

            InventoryManager.instance.ClearSelectedItem();
        }

        public void PlayGame()
        {
            SceneData.sceneToLoad = 2;
            SceneData.showInfo = false;
            SceneManager.LoadScene(1);
        }

        public void QuitGame() => Application.Quit();
    }
}