﻿using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComBlitz.HomeScene
{
    public class PlayAndQuit : MonoBehaviour
    {
        public Texture2D cursorTexture;

        private void Start() => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

        public void PlayGame()
        {
            SceneData.sceneToLoad = 2;
            SceneData.showInfo = false;
            SceneManager.LoadScene(1);
        }

        public void QuitGame() => Application.Quit();
    }
}