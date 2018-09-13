using ComBlitz.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene
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

        private void Start()
        {
            currentKills = 0;
            currentTime = 0;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;

            timeText.text = $"Time : {ExtensionFunctions.Format2DecimalPlace(currentTime)} s";
            killsText.text = $"Kills : {currentKills}";
        }

        public void AddKill() => currentKills += 1;

        public float GetCurrentTime() => currentTime;

        public int GetCurrentKills() => currentKills;
    }
}