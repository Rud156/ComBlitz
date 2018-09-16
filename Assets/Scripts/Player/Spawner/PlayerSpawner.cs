using ComBlitz.ConstantData;
using ComBlitz.Extensions;
using ComBlitz.Factories;
using ComBlitz.Resources;
using ComBlitz.Shooters;
using UnityEngine;
using UnityEngine.AI;

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

        public Transform spawnPoint;
        public float overlapSphereRadius = 2f;

        [Header(("Indicator"))] public Renderer spawnIndicator;
        public Material incorrectPositionMaterial;
        public Material correctPositionMaterial;

        private bool objectIsBeingPlaced;
        private string allowedTagName;
        private GameObject objectToBePlaced;
        private Quaternion rotation;

        private string parentTagName;

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

        public void CreateFactoryOrShooter(string collisionTagName, GameObject objectPrefab, string parentTag)
        {
            GameObject objectInstance = Instantiate(objectPrefab, transform.position,
                objectPrefab.transform.rotation);
            rotation = objectPrefab.transform.rotation;

            objectInstance.transform.SetParent(spawnPoint);
            objectInstance.transform.localPosition = Vector3.zero;
            objectInstance.GetComponent<BoxCollider>().enabled = false;
            objectInstance.GetComponent<NavMeshObstacle>().enabled = false;
            objectInstance.GetComponent<Rigidbody>().isKinematic = true;

            objectIsBeingPlaced = true;
            spawnIndicator.gameObject.SetActive(true);
            allowedTagName = collisionTagName;
            objectToBePlaced = objectInstance;

            parentTagName = parentTag;
            spawnIndicator.transform.rotation = objectPrefab.transform.rotation;
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

            if (objectCanBePlaced && Input.GetMouseButton(0))
            {
                float xPos = spawnPoint.position.x;
                float zPos = spawnPoint.position.z;

                Transform spawnerHolder = GameObject.FindGameObjectWithTag(parentTagName).transform;

                objectToBePlaced.transform.SetParent(spawnerHolder);
                objectToBePlaced.transform.position = new Vector3(xPos, 0.25f, zPos);
                objectToBePlaced.transform.rotation = rotation;
                objectToBePlaced.GetComponent<BoxCollider>().enabled = true;
                objectToBePlaced.GetComponent<NavMeshObstacle>().enabled = true;
                objectToBePlaced.GetComponent<Rigidbody>().isKinematic = false;

                if (parentTagName == TagManager.FactoryHolder)
                    objectToBePlaced.GetComponent<FactoriesCreator>().StartSpawn();
                else
                    objectToBePlaced.GetComponent<ShooterTargetEnemy>().StartShooting();

                ShopManager.instance.UseOrbToPlaceSelectedObject();
                objectIsBeingPlaced = false;
                spawnIndicator.gameObject.SetActive(false);
            }
        }
    }
}