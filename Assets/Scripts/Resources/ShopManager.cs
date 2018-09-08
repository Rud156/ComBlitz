using UnityEngine;
using UnityEngine.UI;

namespace DeBomb.Resources
{
    public class ShopManager : MonoBehaviour
    {
        [System.Serializable]
        public struct ShopItem
        {
            public int greenOrbsCount;
            public int orangeOrbsCount;
            public int redOrbsCount;
            public GameObject prefab;
            public Button button;
        }

        #region Singleton

        public static ShopManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        [Header("Grounds")]
        public ShopItem grassGround;

        public ShopItem dirtGround;
        public ShopItem lavaGround;

        [Header("Enemy Factories")]
        public ShopItem knightFactory;

        public ShopItem orcFactory;
        public ShopItem soldierFactory;

        [Header("Shooters")]
        public ShopItem bulletShooter;

        public ShopItem laserShooter;
        public ShopItem bombShooter;

        private void Update()
        {
            GroundResourceCheck();
            FactoryResourceCheck();
            ShooterResourceCheck();
        }

        #region Ground

        public void GrassGroundOnClick()
        {
            Debug.Log("Grass Ground Clicked");
        }

        public void DirtGroundOnClick()
        {
            Debug.Log("Dirt Ground Clicked");
        }

        public void LavaGroundOnClick()
        {
            Debug.Log("Lava Ground Clicked");
        }

        private void GroundResourceCheck()
        {
            grassGround.button.interactable = ResourceAvailable(grassGround);
            dirtGround.button.interactable = ResourceAvailable(dirtGround);
            lavaGround.button.interactable = ResourceAvailable(lavaGround);
        }

        #endregion Ground

        #region Factory

        public void KnightFactoryOnClick()
        {
            Debug.Log("Knight Factory Clicked");
        }

        public void OrcFactoryOnClick()
        {
            Debug.Log("Orc Factory Clicked");
        }

        public void SoldierFactoryOnClick()
        {
            Debug.Log("Soldier Factory Clicked");
        }

        private void FactoryResourceCheck()
        {
            knightFactory.button.interactable = ResourceAvailable(knightFactory);
            soldierFactory.button.interactable = ResourceAvailable(soldierFactory);
            orcFactory.button.interactable = ResourceAvailable(orcFactory);
        }

        #endregion Factory

        #region Shooter

        public void BulletShooterOnClick()
        {
            Debug.Log("Bullet Shooter Clicked");
        }

        public void LaserShooterOnClick()
        {
            Debug.Log("Laser Shooter Clicked");
        }

        public void BombShooterOnClick()
        {
            Debug.Log("Bomb Shooter Clicked");
        }

        private void ShooterResourceCheck()
        {
            bulletShooter.button.interactable = ResourceAvailable(bulletShooter);
            laserShooter.button.interactable = ResourceAvailable(laserShooter);
            bombShooter.button.interactable = ResourceAvailable(bombShooter);
        }

        #endregion Shooter

        private bool ResourceAvailable(ShopItem item)
        {
            int greenOrbCount = item.greenOrbsCount;
            int orangeOrbCount = item.orangeOrbsCount;
            int redOrbCount = item.redOrbsCount;

            bool greenOrbsAvailable = ResourceManager.instance.OrbsAvailable(ResourceManager.OrbType.Green,
                greenOrbCount);
            bool orangeOrbsAvailable = ResourceManager.instance.OrbsAvailable(ResourceManager.OrbType.Orange,
                orangeOrbCount);
            bool redOrbsAvailable = ResourceManager.instance.OrbsAvailable(ResourceManager.OrbType.Red,
                redOrbCount);

            return greenOrbsAvailable && orangeOrbsAvailable && redOrbsAvailable;
        }
    }
}