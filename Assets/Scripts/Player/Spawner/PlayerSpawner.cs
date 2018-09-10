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
        public Material incorrectPositionMaterial;
        public float overlapSphereRadius = 2f;

        private bool objectIsBeingPlaced;
        private Material actualObjectMaterial;
        private string allowedTagName;
        private GameObject objectToBePlaced;
        private Renderer objectRenderer;

        private void Start() => objectIsBeingPlaced = false;

        private void Update()
        {
            if (!objectIsBeingPlaced)
                return;

            CheckAndPlaceObjectInWorld();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShopManager.instance.ClearItemSelection();
                objectIsBeingPlaced = false;
            }
        }

        public void CreateFactoryOrShooter(string collisionTagName, GameObject objectPrefab)
        {
            GameObject objectInstance = Instantiate(objectPrefab, transform.position,
                objectPrefab.transform.rotation);
            objectInstance.transform.SetParent(spawnPoint);
            objectInstance.transform.localPosition = Vector3.zero;

            objectIsBeingPlaced = true;
            allowedTagName = collisionTagName;
            objectToBePlaced = objectInstance;
            objectRenderer = objectToBePlaced.GetComponent<Renderer>();
            actualObjectMaterial = objectRenderer.material;
        }

        private void CheckAndPlaceObjectInWorld()
        {
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, overlapSphereRadius);
            bool objectCanBePlaced = false;

            if (colliders.Length > 1 || colliders.Length == 0)
                objectRenderer.material = incorrectPositionMaterial;
            else if (colliders[0].CompareTag(allowedTagName))
            {
                objectRenderer.material = actualObjectMaterial;
                objectCanBePlaced = true;
            }
            else
                objectRenderer.material = incorrectPositionMaterial;

            if (objectCanBePlaced && Input.GetMouseButton(0))
            {
                float xPos = ExtensionFunctions.GetClosestMultiple(spawnPoint.position.x);
                float zPos = ExtensionFunctions.GetClosestMultiple(spawnPoint.position.z);

                objectToBePlaced.transform.SetParent(spawnerHolder);
                objectToBePlaced.transform.position = new Vector3(xPos, 0, zPos);

                ShopManager.instance.UseOrbToPlaceSelectedObject();
                objectIsBeingPlaced = false;
            }
        }
    }
}