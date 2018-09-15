using UnityEngine;

namespace ComBlitz.Scene.HomeScene
{
    public class CloseHelpMenu : MonoBehaviour
    {
        public GameObject homeMenu;
        public GameObject helpMenu;

        private void Update()
        {
            if (!Input.GetKey(KeyCode.Escape))
                return;

            homeMenu.SetActive(true);
            helpMenu.SetActive(false);
        }
    }
}