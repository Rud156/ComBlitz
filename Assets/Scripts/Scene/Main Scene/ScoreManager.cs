using ComBlitz.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene.MainScene
{
    public class ScoreManager : MonoBehaviour
    {
        #region Singleton

        public static ScoreManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        [Header("UI Display")] public Text killsText;
        public Text timeText;

        private int currentKills;
        private float currentTime;
        private bool countScore;

        private void Start()
        {
            currentKills = 0;
            currentTime = 0;
            countScore = false;
        }

        private void Update()
        {
            timeText.text = $"Time : {ExtensionFunctions.Format2DecimalPlace(currentTime)} s";
            killsText.text = $"Kills : {currentKills}";

            if (countScore)
                currentTime += Time.deltaTime;
        }

        public void StartScoring() => countScore = true;

        public void StopScoring() => countScore = false;

        public void AddKill() => currentKills = countScore ? currentKills + 1 : currentKills;

        public float GetCurrentTime() => currentTime;

        public int GetCurrentKills() => currentKills;
    }
}