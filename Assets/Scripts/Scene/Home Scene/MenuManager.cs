using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComBlitz.Scene.HomeScene
{
    public class MenuManager : MonoBehaviour
    {
        #region Singleton

        public static MenuManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public Texture2D cursorTexture;
        public GameObject homeMenu;
        public Toggle movementToggle;

        [Header("Help Menu")] public GameObject helpMenu;
        public GameObject controlsMenu;
        public GameObject inventoryMenu;

        private bool toggleOn;
        private bool inventoryActive;

        private void Start()
        {
            int toggleValue = 0;
            if (PlayerPrefs.HasKey(SceneData.MovementPlayerPref))
                toggleValue = PlayerPrefs.GetInt(SceneData.MovementPlayerPref);

            movementToggle.isOn = toggleValue != 0;
            toggleOn = toggleValue != 0;

            inventoryActive = false;

            Cursor.SetCursor(cursorTexture, new Vector2(15, 15), CursorMode.Auto);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseHelpMenu();
        }

        public bool InventoryActive() => inventoryActive;

        public void HandlePlayerMovementTypeChange()
        {
            toggleOn = !toggleOn;
            PlayerPrefs.SetInt(SceneData.MovementPlayerPref, toggleOn ? 1 : 0);
            movementToggle.isOn = toggleOn;
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
            inventoryActive = true;
        }

        public void CloseHelpMenu()
        {
            homeMenu.SetActive(true);
            helpMenu.SetActive(false);
            controlsMenu.SetActive(true);
            inventoryMenu.SetActive(false);

            inventoryActive = false;
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