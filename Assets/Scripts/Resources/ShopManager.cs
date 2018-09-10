using ComBlitz.ConstantData;
using ComBlitz.Player.Spawner;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Resources
{
    public class ShopManager : MonoBehaviour
    {
        public enum GroundTypes
        {
            LavaGround,
            GrassGround,
            DirtGround,
            None
        };

        public enum ItemType
        {
            Ground,
            Shooter,
            Factory
        };

        [System.Serializable]
        public class ShopItem
        {
            public int greenOrbsCount;
            public int orangeOrbsCount;
            public int redOrbsCount;

            [TextArea]
            public string description;

            public GroundTypes groundToBePlacedOn;
            public ItemType itemType;

            public GameObject prefab;
            public Button button;
            public Image itemBorderImage;
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

        [Header("Image States")]
        public Sprite defaultBorder;

        public Sprite notAvailableBorder;
        public Sprite selectedBorder;

        [Header("Item Details")]
        public GameObject itemDetailsParent;

        public Text itemDescription;

        public Text itemOrangeOrbCount;
        public Text itemRedOrbCount;
        public Text itemGreenOrbCount;

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

        private void Start() => SelectedItem = null;

        private void Update()
        {
            CheckAndRenderAllShopItems();
            RenderSelecteditem();
            SetActiveItemToUI();
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

        private void RenderSelecteditem()
        {
            if (SelectedItem == null)
                itemDetailsParent.SetActive(false);
            else
            {
                itemDetailsParent.SetActive(true);
                itemDescription.text = SelectedItem.description;
                itemGreenOrbCount.text = $"X {SelectedItem.greenOrbsCount}";
                itemRedOrbCount.text = $"X {SelectedItem.redOrbsCount}";
                itemOrangeOrbCount.text = $"X {SelectedItem.orangeOrbsCount}";
            }
        }

        // This method is called from the Select Item Button
        public void UseItem()
        {
            if (SelectedItem.itemType == ItemType.Ground)
                PlayerSpawnGroundController.instance.CreateGroundInWorld(SelectedItem.prefab);
            else
            {
                string tagName = "";
                if (SelectedItem.groundToBePlacedOn == GroundTypes.DirtGround)
                    tagName = TagManager.DirtGround;
                else if (SelectedItem.groundToBePlacedOn == GroundTypes.GrassGround)
                    tagName = TagManager.GrassGround;
                else if (SelectedItem.groundToBePlacedOn == GroundTypes.LavaGround)
                    tagName = TagManager.LavaGround;
                else
                    return;

                PlayerSpawner.instance.CreateFactoryOrShooter(tagName, SelectedItem.prefab);
            }
        }

        public void UseOrbToPlaceSelectedObject()
        {
            ResourceManager.instance.UseOrbs(ResourceManager.OrbType.Orange, SelectedItem.orangeOrbsCount);
            ResourceManager.instance.UseOrbs(ResourceManager.OrbType.Red, SelectedItem.redOrbsCount);
            ResourceManager.instance.UseOrbs(ResourceManager.OrbType.Green, SelectedItem.greenOrbsCount);

            SelectedItem = null;
        }

        public void ClearItemSelection() => SelectedItem = null;

        private void SetActiveItemToUI()
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

        private void CheckAndRenderAllShopItems()
        {
            // Grounds
            RenderInactiveWhenResourceNotAvailable(grassGround);
            RenderInactiveWhenResourceNotAvailable(dirtGround);
            RenderInactiveWhenResourceNotAvailable(lavaGround);

            // Factories
            RenderInactiveWhenResourceNotAvailable(knightFactory);
            RenderInactiveWhenResourceNotAvailable(soldierFactory);
            RenderInactiveWhenResourceNotAvailable(orcFactory);

            // Shooters
            RenderInactiveWhenResourceNotAvailable(bulletShooter);
            RenderInactiveWhenResourceNotAvailable(laserShooter);
            RenderInactiveWhenResourceNotAvailable(bombShooter);
        }

        private void RenderInactiveWhenResourceNotAvailable(ShopItem item)
        {
            bool resourceAvailable = ResourceAvailable(item);
            if (!resourceAvailable)
            {
                item.button.interactable = false;
                item.itemBorderImage.sprite = notAvailableBorder;
            }
            else
                item.button.interactable = true;
        }

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