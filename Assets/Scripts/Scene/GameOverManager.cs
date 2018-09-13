using System.Collections;
using ComBlitz.ConstantData;
using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene
{
    public class GameOverManager : MonoBehaviour
    {
        #region Singleton

        private static GameOverManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        [Header("UI Display")] public Animator displayTextAnimator;
        public Text displayText;

        private GameObject baseObject;
        private GameObject playerObject;
        private bool sceenSwitcherActivated;

        private void Start()
        {
            baseObject = GameObject.FindGameObjectWithTag(TagManager.Base);
            playerObject = GameObject.FindGameObjectWithTag(TagManager.Player);

            sceenSwitcherActivated = false;

            StartCoroutine(DisplayStartingText());
        }

        private void Update()
        {
            bool gameOver = CheckPlayerAndBase();
            if (gameOver && !sceenSwitcherActivated)
            {
                StartCoroutine(SwitchScreenAndGoToMainMenu());
                sceenSwitcherActivated = true;
            }
        }

        private IEnumerator DisplayStartingText()
        {
            yield return new WaitForSeconds(2f);
            DisplayTextContent("Protect the Base and make sure not to fall off");
        }

        private IEnumerator SwitchScreenAndGoToMainMenu()
        {
            DisplayTextContent("Game Over");
            
            yield return new WaitForSeconds(3);

            NextSceneData.sceneTime = ScoreManager.instance.GetCurrentTime();
            NextSceneData.sceneKills = ScoreManager.instance.GetCurrentKills();
            NextSceneData.showInfo = true;

            PauseAndResume.instance.GoToMainMenu();
        }

        private bool CheckPlayerAndBase() => baseObject == null || playerObject == null;

        private void DisplayTextContent(string text)
        {
            displayText.text = text;
            displayTextAnimator.SetTrigger("FadeInOut");
        }
    }
}