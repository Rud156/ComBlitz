using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        #region Singleton

        public static ResourceManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        [Header("Starting Resources")]
        public int initialGreenOrbCount;

        public int initialOrangeOrbCount;
        public int initialRedOrbCount;

        [Header("UI Display")]
        public Text greenOrbText;

        public Text orangeOrbText;
        public Text redOrbText;

        public enum OrbType
        {
            Green,
            Red,
            Orange
        };

        private int greenOrbsCount;
        private int redOrbsCount;
        private int orangeOrbsCount;

        private void Start()
        {
            greenOrbsCount = initialGreenOrbCount;
            redOrbsCount = initialRedOrbCount;
            orangeOrbsCount = initialOrangeOrbCount;
        }

        private void Update() => DisplayResourcesOnUI();

        private void DisplayResourcesOnUI()
        {
            greenOrbText.text = $"X {greenOrbsCount}";
            orangeOrbText.text = $"X {orangeOrbsCount}";
            redOrbText.text = $"X {redOrbsCount}";
        }

        public void AddOrb(OrbType orbType, int totalCount)
        {
            switch (orbType)
            {
                case OrbType.Green:
                    greenOrbsCount += totalCount;
                    break;

                case OrbType.Orange:
                    orangeOrbsCount += totalCount;
                    break;

                case OrbType.Red:
                    redOrbsCount += totalCount;
                    break;
            }
        }

        public bool UseOrbs(OrbType orbType, int totalCount)
        {
            bool orbsUsedSuccessfully = true;

            switch (orbType)
            {
                case OrbType.Green:
                    orbsUsedSuccessfully = greenOrbsCount - totalCount >= 0;
                    if (orbsUsedSuccessfully)
                        greenOrbsCount -= totalCount;
                    break;

                case OrbType.Orange:
                    orbsUsedSuccessfully = orangeOrbsCount - totalCount >= 0;
                    if (orbsUsedSuccessfully)
                        orangeOrbsCount -= totalCount;
                    break;

                case OrbType.Red:
                    orbsUsedSuccessfully = redOrbsCount - totalCount >= 0;
                    if (orbsUsedSuccessfully)
                        redOrbsCount -= totalCount;
                    break;
            }

            return orbsUsedSuccessfully;
        }

        public bool OrbsAvailable(OrbType orbType, int totalCount)
        {
            bool orbsAvailable = true;

            switch (orbType)
            {
                case OrbType.Green:
                    orbsAvailable = greenOrbsCount - totalCount >= 0;
                    break;

                case OrbType.Orange:
                    orbsAvailable = orangeOrbsCount - totalCount >= 0;
                    break;

                case OrbType.Red:
                    orbsAvailable = redOrbsCount - totalCount >= 0;
                    break;
            }

            return orbsAvailable;
        }
    }
}