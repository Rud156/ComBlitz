using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeBomb.Ground
{
    public class GroundManager : MonoBehaviour
    {
        #region Singleton

        public static GroundManager instance;

        private void Awake() => instance = this;

        #endregion Singleton

        public delegate void CreateGroundInstance(GameObject groundObject);

        public CreateGroundInstance createGroundInstance;

        [Header("Ground Spawn Stats")]
        public Transform groundParent;

        public float fallWaitTime = 14;
        public int totalGroundsToFall = 7;

        [Header("Grounds")]
        public GameObject grassGround;

        public GameObject lavaGround;
        public GameObject dirtGround;

        [Header("Debug")]
        public bool fallOnStart;

        private List<GameObject> grounds;
        private List<Rigidbody> groundsToFall;
        private Coroutine coroutine;

        private int currentGroundChild;
        private float waitBetweenEachEnumRate;
        private bool listCompleted;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            grounds = new List<GameObject>();
            groundsToFall = new List<Rigidbody>();

            currentGroundChild = 0;
            listCompleted = false;

            grounds.Add(groundParent.GetChild(currentGroundChild).gameObject);
            currentGroundChild += 1;

            if (fallOnStart)
                StartGroundFall();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            AddGroundChildToList();
            CreateGround();
        }

        public void StartGroundFall()
        {
            groundsToFall.Clear();
            waitBetweenEachEnumRate = fallWaitTime / totalGroundsToFall;

            coroutine = StartCoroutine(MakeGroundsFall());
        }

        public void StopGroundFall() => StopCoroutine(coroutine);

        public void AddGround(GameObject ground) => grounds.Add(ground);

        private void AddGroundChildToList()
        {
            if (listCompleted)
                return;

            if (currentGroundChild < groundParent.childCount)
            {
                grounds.Add(groundParent.GetChild(currentGroundChild).gameObject);
                currentGroundChild += 1;
            }
            else
                listCompleted = true;
        }

        private void CreateGround()
        {
            // Use Resource Manager to check for sufficient resources

            if (createGroundInstance == null)
                return;

            GameObject ground;

            if (Input.GetKeyDown(KeyCode.Z))
                ground = Instantiate(dirtGround, transform.position, Quaternion.identity);
            else if (Input.GetKeyDown(KeyCode.X))
                ground = Instantiate(grassGround, transform.position, Quaternion.identity);
            else if (Input.GetKeyDown(KeyCode.C))
                ground = Instantiate(lavaGround, transform.position, Quaternion.identity);
            else
                return;

            ground.transform.SetParent(groundParent);
            createGroundInstance(ground);
        }

        private IEnumerator MakeGroundsFall()
        {
            while (true)
            {
                int randomValue = Random.Range(0, 1000);
                int randomIndex = randomValue % grounds.Count;

                groundsToFall.Add(grounds[randomIndex].GetComponent<Rigidbody>());

                grounds[randomIndex].GetComponent<Renderer>().material.color = Color.black;
                grounds.RemoveAt(randomIndex);

                if (groundsToFall.Count >= totalGroundsToFall)
                {
                    foreach (Rigidbody rb in groundsToFall)
                        rb.isKinematic = false;

                    groundsToFall.Clear();
                }

                yield return new WaitForSeconds(waitBetweenEachEnumRate);
            }
        }
    }
}