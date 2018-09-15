using UnityEngine;

namespace ComBlitz.Scene.MainScene
{
    public class MakeCursorVisibleOnStart : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}