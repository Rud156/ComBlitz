using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Singleton

    public ResourceManager instance;

    private void Awake()
    {
        instance = this;

        if (instance != this)
            Destroy(gameObject);
    }

    #endregion Singleton

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
        greenOrbsCount = 0;
        redOrbsCount = 0;
        orangeOrbsCount = 0;
    }

    public void AddOrb(OrbType orbType, int totalCount = 1)
    {
        switch (orbType)
        {
            case OrbType.Green:
                greenOrbsCount += 1;
                break;

            case OrbType.Orange:
                orangeOrbsCount += 1;
                break;

            case OrbType.Red:
                redOrbsCount += 1;
                break;
        }
    }

    public bool UseOrbs(OrbType orbType, int totalCount = 1)
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

    public bool OrbsAvailable(OrbType orbType, int totalCount = 0)
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