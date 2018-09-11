using ComBlitz.Extensions;
using ComBlitz.Ground;
using ComBlitz.Resources;
using UnityEngine;

namespace ComBlitz.Player.Spawner
{
    public class PlayerSpawnGroundController : MonoBehaviour
    {
        #region Singleton

        public static PlayerSpawnGroundController instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public Transform groundHolder;
        public Transform groundTracker;
        public Renderer groundIndicator;
        public float initalSpawnHeightAbove = 1f;
        public Material incorrectGroundMaterial;
        public Material correctGroundMaterial;
        public float overlapSphereRadius = 2f;

        private bool groundIsBeingPlaced;
        private GameObject groundToBePlaced;

        private void Start() => groundIsBeingPlaced = false;

        private void Update()
        {
            if (!groundIsBeingPlaced)
                return;

            CheckAndCreateGroundInWorld();
        }

        public void DestroyNotPlacedGround()
        {
            Destroy(groundToBePlaced);
            groundIsBeingPlaced = false;
            groundIndicator.gameObject.SetActive(false);
        }

        public void CreateGroundInWorld(GameObject groundPrefab)
        {
            GameObject groundInstance = Instantiate(groundPrefab, transform.position, Quaternion.identity);
            groundInstance.transform.SetParent(groundTracker);
            groundInstance.transform.localPosition = Vector3.zero + Vector3.up * initalSpawnHeightAbove;

            groundIsBeingPlaced = true;
            groundIndicator.gameObject.SetActive(true);
            groundToBePlaced = groundInstance;
        }

        private void CheckAndCreateGroundInWorld()
        {
            Collider[] colliders = Physics.OverlapSphere(groundTracker.position, overlapSphereRadius);

            bool clear = false;

            if (colliders.Length == 0)
            {
                clear = true;
                groundIndicator.material = incorrectGroundMaterial;
            }
            else
                groundIndicator.material = correctGroundMaterial;

            if (clear && Input.GetMouseButtonDown(0))
            {
                float xPos = ExtensionFunctions.GetClosestMultiple(groundTracker.position.x);
                float zPos = ExtensionFunctions.GetClosestMultiple(groundTracker.position.z);

                groundToBePlaced.transform.SetParent(groundHolder);
                groundToBePlaced.transform.position = new Vector3(xPos, 0, zPos);

                ShopManager.instance.UseOrbToPlaceSelectedObject();
                groundIsBeingPlaced = false;

                GroundManager.instance.AddGround(groundToBePlaced);
            }
        }
    }
}