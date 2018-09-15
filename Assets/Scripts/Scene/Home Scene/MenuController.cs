using UnityEngine;

namespace ComBlitz.Scene.HomeScene
{
    public class MenuController : MonoBehaviour
    {
        public GameObject homeMenu;

        [Header("Help Help")] public GameObject helpMenu;
        public GameObject controlsMenu;
        public GameObject inventoryMenu;

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
        }
    }
}