using ComBlitz.Scene.Data;
using ComBlitz.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ComBlitz.StoryScene
{
    public class StorySceneManager : MonoBehaviour
    {
        #region Singleton

        public static StorySceneManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public Rigidbody baseRocketBody;

        private bool sceneFadeOutStarted;

        private void Start()
        {
            baseRocketBody.isKinematic = true;
            sceneFadeOutStarted = false;
            
            DialogueManager.instance.dialogueComplete += FadeOutScene;
            Fader.instance.fadeInComplete += ActivateScene;
            Fader.instance.fadeOutComplete += DeactivateScene;
            Fader.instance.StartFadeIn();
        }

        private void OnDestroy()
        {
            DialogueManager.instance.dialogueComplete -= FadeOutScene;
            Fader.instance.fadeInComplete -= ActivateScene;
            Fader.instance.fadeOutComplete -= DeactivateScene;
        }

        public void SkipScene() => FadeOutScene();

        private void FadeOutScene()
        {
            if(sceneFadeOutStarted)
                return;

            sceneFadeOutStarted = true;
            Fader.instance.StartFadeOut();
        }

        private void ActivateScene()
        {
            DialogueManager.instance.StartDialogues();
            baseRocketBody.isKinematic = false;
        }
        
        private void DeactivateScene()
        {
            SceneData.sceneToLoad = 3;
            SceneManager.LoadScene(1);
        }
    }
}