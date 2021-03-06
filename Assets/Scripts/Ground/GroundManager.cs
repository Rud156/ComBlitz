﻿using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.AI;

namespace ComBlitz.Ground
{
    public class GroundManager : MonoBehaviour
    {
        #region Singleton

        public static GroundManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        [Header("Ground Spawn Stats")] public Transform groundParent;
        public float fallWaitTime = 14;
        public int totalGroundsToFall = 7;
        public Material brokenGroundMaterial;

        [Header(("NavMesh"))] public NavMeshSurface navMeshSurface;
        public float waitTimeAfterFalling = 0.3f;

        [Header("Debug")] public bool fallOnStart;

        private List<GameObject> grounds;
        private List<Rigidbody> groundsToFall;
        private Coroutine coroutine;

        private int currentGroundChild;
        private float waitBetweenEachEnumRate;
        private bool listCompleted;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update
        /// methods is called the first time.
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
        private void Update() => AddGroundChildToList();

        public void ActivateGroundFalling() => StartGroundFall();

        public void StartGroundFall()
        {
            groundsToFall.Clear();
            waitBetweenEachEnumRate = fallWaitTime / totalGroundsToFall;

            coroutine = StartCoroutine(MakeGroundsFall());
        }

        public void StopGroundFall() => StopCoroutine(coroutine);

        public void AddGround(GameObject ground)
        {
            grounds.Add(ground);
            navMeshSurface.BuildNavMesh();
        }

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

        private IEnumerator MakeGroundsFall()
        {
            while (true)
            {
                int randomValue = Random.Range(0, 1000);
                int randomIndex = randomValue % grounds.Count;

                groundsToFall.Add(grounds[randomIndex].GetComponent<Rigidbody>());

                grounds[randomIndex].GetComponent<Renderer>().material = brokenGroundMaterial;
                grounds.RemoveAt(randomIndex);

                if (groundsToFall.Count >= totalGroundsToFall)
                {
                    foreach (Rigidbody rb in groundsToFall)
                        rb.isKinematic = false;

                    groundsToFall.Clear();
                    CameraShaker.Instance.ShakeOnce(5, 5, 2, 0.3f);
                    StartCoroutine(BuildNavMesh());
                }

                yield return new WaitForSeconds(waitBetweenEachEnumRate);
            }
        }

        private IEnumerator BuildNavMesh()
        {
            yield return new WaitForSeconds(waitTimeAfterFalling);
            navMeshSurface.BuildNavMesh();
        }
    }
}