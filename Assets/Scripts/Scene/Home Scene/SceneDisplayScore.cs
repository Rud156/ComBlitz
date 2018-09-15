using ComBlitz.Extensions;
using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene.HomeScene
{
    public class SceneDisplayScore : MonoBehaviour
    {
        [Header("UI Display")] public Text killsText;
        public Text timeText;

        private void Start()
        {
            int kills = 0;
            if (PlayerPrefs.HasKey(SceneData.KillsPlayerPref))
                kills = PlayerPrefs.GetInt(SceneData.KillsPlayerPref);

            float survivedTime = 0;
            if (PlayerPrefs.HasKey(SceneData.TimePlayerPref))
                survivedTime = PlayerPrefs.GetFloat(SceneData.TimePlayerPref);

            killsText.text = $"Highest Kills : {kills}";
            timeText.text = $"Longest Survived Time : {ExtensionFunctions.Format2DecimalPlace(survivedTime)} s";
        }
    }
}