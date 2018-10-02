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
        public Transform spawnPoint;
        public float initalSpawnHeightAbove = 1f;
        public float overlapSphereRadius = 2f;

        [Header(("Indicator"))] public Renderer groundIndicator;
        public Material incorrectGroundMaterial;
        public Material correctGroundMaterial;

        private bool groundIsBeingPlaced;
        private GameObject groundToBePlaced;
        private readonly LayerMask layerMask = 1 << 9 | 1 << 10 | 1 << 13 | 1 << 14;

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
            groundInstance.transform.SetParent(spawnPoint);
            groundInstance.transform.localPosition = Vector3.zero + Vector3.up * initalSpawnHeightAbove;
            groundInstance.GetComponent<BoxCollider>().enabled = false;

            groundIsBeingPlaced = true;
            groundIndicator.gameObject.SetActive(true);
            groundToBePlaced = groundInstance;

            groundIndicator.transform.rotation = groundPrefab.transform.rotation;
        }

        private void CheckAndCreateGroundInWorld()
        {
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, overlapSphereRadius, layerMask);
            bool clear = false;

            if (colliders.Length == 0)
            {
                clear = true;
                groundIndicator.material = correctGroundMaterial;
            }
            else
                groundIndicator.material = incorrectGroundMaterial;

            if (clear && Input.GetMouseButtonDown(0))
            {
                float xPos = spawnPoint.position.x;
                float zPos = spawnPoint.position.z;

                groundToBePlaced.transform.SetParent(groundHolder);
                groundToBePlaced.transform.position = new Vector3(xPos, 0, zPos);
                groundToBePlaced.transform.rotation = Quaternion.identity;
                groundToBePlaced.GetComponent<BoxCollider>().enabled = true;

                ShopManager.instance.UseOrbToPlaceSelectedObject();
                groundIsBeingPlaced = false;
                groundIndicator.gameObject.SetActive(false);

                GroundManager.instance.AddGround(groundToBePlaced);
            }
        }
    }
}