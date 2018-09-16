using ComBlitz.Resources;
using UnityEngine;

namespace ComBlitz.Resources
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/Item")]
    public class InventoryItem : ScriptableObject
    {
        public GameObject itemPrefab;

        public int greenOrbCount;
        public int orangeOrbCount;
        public int redOrbCount;

        [TextArea] public string description;

        public ShopManager.GroundTypes groundToBePlacedOn;
        public ShopManager.ItemType itemType;
    }
}