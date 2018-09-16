using ComBlitz.InventoryObjects;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene.HomeScene
{
    public class InventoryManager : MonoBehaviour
    {
        #region Singleton

        public static InventoryManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        [System.Serializable]
        public class ShopItem
        {
            public Image itemBorderImage;
            public InventoryItem item;
        }

        [Header("Grounds")] public ShopItem grassGround;
        public ShopItem dirtGround;
        public ShopItem lavaGround;

        [Header("Enemy Factories")] public ShopItem knightFactory;
        public ShopItem orcFactory;
        public ShopItem soldierFactory;

        [Header("Shooters")] public ShopItem bulletShooter;
        public ShopItem laserShooter;
        public ShopItem bombShooter;

        [Header("Image States")] public Sprite defaultBorder;
        public Sprite selectedBorder;

        [Header("Inventory Details")] public Text itemDescription;
        public GameObject itemDetailsParent;

        private ShopItem _selectedItem;

        private ShopItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                ResetBorders();
                _selectedItem = value;
            }
        }

        private void Update()
        {
            RenderSelectedItem();
            SetActiveItemToUi();
        }

        #region Ground

        public void GrassGroundOnClick() => SelectedItem = grassGround;

        public void DirtGroundOnClick() => SelectedItem = dirtGround;

        public void LavaGroundOnClick() => SelectedItem = lavaGround;

        #endregion Ground

        #region Factory

        public void KnightFactoryOnClick() => SelectedItem = knightFactory;

        public void OrcFactoryOnClick() => SelectedItem = orcFactory;

        public void SoldierFactoryOnClick() => SelectedItem = soldierFactory;

        #endregion Factory

        #region Shooter

        public void BulletShooterOnClick() => SelectedItem = bulletShooter;

        public void LaserShooterOnClick() => SelectedItem = laserShooter;

        public void BombShooterOnClick() => SelectedItem = bombShooter;

        #endregion Shooter

        public void ClearSelectedItem() => SelectedItem = null;

        private void RenderSelectedItem()
        {
            if (SelectedItem == null)
                itemDetailsParent.SetActive(false);
            else
            {
                itemDetailsParent.SetActive(true);
                itemDescription.text = SelectedItem.item.description;
            }
        }

        private void SetActiveItemToUi()
        {
            // Grounds
            SetItemActiveBasedOnSelection(grassGround);
            SetItemActiveBasedOnSelection(dirtGround);
            SetItemActiveBasedOnSelection(lavaGround);

            // Factories
            SetItemActiveBasedOnSelection(knightFactory);
            SetItemActiveBasedOnSelection(soldierFactory);
            SetItemActiveBasedOnSelection(orcFactory);

            // Shooters
            SetItemActiveBasedOnSelection(bulletShooter);
            SetItemActiveBasedOnSelection(laserShooter);
            SetItemActiveBasedOnSelection(bombShooter);
        }

        private void SetItemActiveBasedOnSelection(ShopItem item)
        {
            if (SelectedItem == null)
            {
                item.itemBorderImage.sprite = defaultBorder;
                return;
            }

            if (SelectedItem.Equals(item))
                item.itemBorderImage.sprite = selectedBorder;
            else
                item.itemBorderImage.sprite = defaultBorder;
        }

        private void ResetBorders()
        {
            // Grounds
            ResetBorder(grassGround);
            ResetBorder(dirtGround);
            ResetBorder(lavaGround);

            // Factories
            ResetBorder(knightFactory);
            ResetBorder(soldierFactory);
            ResetBorder(orcFactory);

            // Shooters
            ResetBorder(bulletShooter);
            ResetBorder(laserShooter);
            ResetBorder(bombShooter);
        }

        private void ResetBorder(ShopItem item) => item.itemBorderImage.sprite = defaultBorder;
    }
}