using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeBomb.Ground
{
    public class GroundFall : MonoBehaviour
    {
        public Transform groundParent;
        public float fallWaitTime = 14;
        public int totalGroundsToFall = 7;

        [Header("Debug")]
        public bool fallOnStart;

        private List<GameObject> grounds;
        private List<Rigidbody> groundsToFall;
        private Coroutine coroutine;

        private int currentGroundChild;
        private float waitBetweenEachEnumRate;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        private void Start()
        {
            grounds = new List<GameObject>();
            groundsToFall = new List<Rigidbody>();

            currentGroundChild = 0;
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
            if (groundParent.childCount > currentGroundChild)
            {
                grounds.Add(groundParent.GetChild(currentGroundChild).gameObject);
                currentGroundChild += 1;
            }
        }

        public void StartGroundFall()
        {
            groundsToFall.Clear();
            waitBetweenEachEnumRate = fallWaitTime / totalGroundsToFall;

            coroutine = StartCoroutine(MakeGroundsFall());
        }

        public void StopGroundFall() => StopCoroutine(coroutine);

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