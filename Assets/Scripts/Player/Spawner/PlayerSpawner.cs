using ComBlitz.Extensions;
using ComBlitz.Resources;
using UnityEngine;

namespace ComBlitz.Player.Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        #region Singleton

        public static PlayerSpawner instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public Transform spawnerHolder;
        public Transform spawnPoint;
        public Renderer spawnIndicator;
        public Material incorrectPositionMaterial;
        public Material correctPositionMaterial;
        public float overlapSphereRadius = 2f;

        private bool objectIsBeingPlaced;
        private string allowedTagName;
        private GameObject objectToBePlaced;

        private void Start() => objectIsBeingPlaced = false;

        private void Update()
        {
            if (!objectIsBeingPlaced)
                return;

            CheckAndPlaceObjectInWorld();
        }

        public void DestroyNotPlacedItem()
        {
            Destroy(objectToBePlaced);
            objectIsBeingPlaced = false;
            spawnIndicator.gameObject.SetActive(false);
        }

        public void CreateFactoryOrShooter(string collisionTagName, GameObject objectPrefab)
        {
            GameObject objectInstance = Instantiate(objectPrefab, transform.position,
                objectPrefab.transform.rotation);
            objectInstance.transform.SetParent(spawnPoint);
            objectInstance.transform.localPosition = Vector3.zero;

            objectIsBeingPlaced = true;
            spawnIndicator.gameObject.SetActive(true);
            allowedTagName = collisionTagName;
            objectToBePlaced = objectInstance;
        }

        private void CheckAndPlaceObjectInWorld()
        {
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, overlapSphereRadius);
            bool objectCanBePlaced = false;

            if (colliders.Length > 1 || colliders.Length == 0)
                spawnIndicator.material = incorrectPositionMaterial;
            else if (colliders[0].CompareTag(allowedTagName))
            {
                objectCanBePlaced = true;
                spawnIndicator.material = correctPositionMaterial;
            }
            else
                spawnIndicator.material = incorrectPositionMaterial;
            
            Debug.Log($"Length: {colliders.Length}");
            Debug.Log($"Tag: {colliders[0].tag}");
            Debug.Log($"Allowed Tag: {allowedTagName}");

            if (objectCanBePlaced && Input.GetMouseButton(0))
            {
                float xPos = ExtensionFunctions.GetClosestMultiple(spawnPoint.position.x);
                float zPos = ExtensionFunctions.GetClosestMultiple(spawnPoint.position.z);

                objectToBePlaced.transform.SetParent(spawnerHolder);
                objectToBePlaced.transform.position = new Vector3(xPos, 0.25f, zPos);

                ShopManager.instance.UseOrbToPlaceSelectedObject();
                objectIsBeingPlaced = false;
            }
        }
    }
}