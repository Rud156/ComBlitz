using System.Collections;
using ComBlitz.ConstantData;
using ComBlitz.Scene.Data;
using ComBlitz.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene.MainScene
{
    public class SceneTextAndEndManager : MonoBehaviour
    {
        #region Singleton

        private static SceneTextAndEndManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public float waitForFadeOutTime = 3f;

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

            DisplayStartingText();
            Fader.instance.fadeOutComplete += SwitchScreenAndGoToMainMenu;
        }

        private void Update()
        {
            bool gameOver = CheckPlayerAndBase();
            if (gameOver && !sceenSwitcherActivated)
            {
                ScoreManager.instance.StopScoring();
                
                if (baseObject == null)
                    DisplayTextContent("Your base was destroyed ...");
                else
                    DisplayTextContent("You were killed in action ...");
                StartCoroutine(ActivateFadeOut());
                sceenSwitcherActivated = true;
            }
        }

        private void OnDestroy() => Fader.instance.fadeOutComplete -= SwitchScreenAndGoToMainMenu;

        private IEnumerator ActivateFadeOut()
        {
            yield return new WaitForSeconds(waitForFadeOutTime);
            Fader.instance.StartFadeOut();
        }

        private void DisplayStartingText() => DisplayTextContent("Protect the Base and make sure not to fall off");

        private void SwitchScreenAndGoToMainMenu()
        {
            int sceneKills = ScoreManager.instance.GetCurrentKills();
            float sceneTime = ScoreManager.instance.GetCurrentTime();

            SceneData.sceneTime = sceneTime;
            SceneData.sceneKills = sceneKills;
            SceneData.showInfo = true;

            SaveScore(sceneKills, sceneTime);
            PauseAndResume.instance.GoToMainMenu();
        }

        private void SaveScore(int sceneKills, float sceneTime)
        {
            int kills = 0;
            if (PlayerPrefs.HasKey(SceneData.KillsPlayerPref))
                kills = PlayerPrefs.GetInt(SceneData.KillsPlayerPref);

            float survivedTime = 0;
            if (PlayerPrefs.HasKey(SceneData.TimePlayerPref))
                survivedTime = PlayerPrefs.GetFloat(SceneData.TimePlayerPref);

            if (sceneKills > kills)
                kills = sceneKills;
            if (sceneTime > survivedTime)
                survivedTime = sceneTime;

            PlayerPrefs.SetInt(SceneData.KillsPlayerPref, kills);
            PlayerPrefs.SetFloat(SceneData.TimePlayerPref, survivedTime);
        }

        private bool CheckPlayerAndBase() => baseObject == null || playerObject == null;

        private void DisplayTextContent(string text)
        {
            displayText.text = text;
            displayTextAnimator.SetTrigger("FadeInOut");
        }
    }
}