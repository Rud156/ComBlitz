using ComBlitz.Player.Shooter;
using ComBlitz.Resources;
using UnityEngine;

namespace ComBlitz.Scene
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton

        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public PlayerShootController playerShooter;

        private bool inventoryItemSelected;
        private bool inventoryOpen;
        private bool pauseMenuOpen;

        private void Start()
        {
            inventoryOpen = false;
            pauseMenuOpen = false;
            inventoryItemSelected = false;
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (inventoryItemSelected)
                DiscardSelectedInventoryItem();

            else if (inventoryOpen)
                CloseInventory();

            else if (pauseMenuOpen)
                ClosePauseMenu();
            else if (!pauseMenuOpen)
                OpenPauseMenu();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus) return;

            if (inventoryOpen)
                CloseInventory();

            OpenPauseMenu();
        }

        #region Inventory

        public void InventoryOpened()
        {
            playerShooter.DeActivateShooting();
            inventoryOpen = true;
        }

        public void InventoryClosed()
        {
            playerShooter.ActivateShooting();
            inventoryOpen = false;
        }

        private void CloseInventory()
        {
            ShopManager.instance.CloseInventory(true);
            InventoryClosed();
        }

        #endregion Inventory

        #region PauseMenu

        public void PauseMenuOpened() => pauseMenuOpen = true;

        public void PauseMenuClose() => pauseMenuOpen = true;

        public void ClosePauseMenu()
        {
            PauseAndResume.instance.ResumeGame();
            pauseMenuOpen = false;
        }

        private void OpenPauseMenu()
        {
            PauseAndResume.instance.PauseGame();
            pauseMenuOpen = true;
        }

        #endregion PauseMenu

        #region InventoryItem

        public void InventoryItemSelected()
        {
            playerShooter.DeActivateShooting();
            inventoryItemSelected = true;

            inventoryOpen = false;
            ShopManager.instance.CloseInventory();
        }

        public void InventoryItemWorkComplete()
        {
            playerShooter.ActivateShooting();
            inventoryItemSelected = false;
        }

        private void DiscardSelectedInventoryItem()
        {
            InventoryItemWorkComplete();
            ShopManager.instance.ClearItemSelectionAndDestroyObject();
        }

        #endregion InventoryItem
    }
}