﻿using System.Collections;
using UnityEngine;

namespace ComBlitz.Factories
{
    public class FactoriesCreator : MonoBehaviour
    {
        [Header("Spawn Objects")]
        public Transform spawnTransform;
        public Transform spawnerHolder;
        public GameObject unit;
        public GameObject spawnEffect;

        [Header("Spawn Stats")]
        public float waitBetweenSpawn = 5f;
        public float waitBeteweenEffectAndSpawn = 0.1f;

        [Header("Debug")]
        public bool spawnOnStart;

        protected Coroutine coroutine;

        private void Start()
        {
            if (spawnOnStart)
                StartSpawn();
        }

        public void StartSpawn() => coroutine = StartCoroutine(SpawnUnits());

        public void StopSpawn() => StopCoroutine(coroutine);
        
        public IEnumerator SpawnUnits()
        {
            Instantiate(spawnEffect, spawnTransform.position, spawnEffect.transform.rotation);

            yield return new WaitForSeconds(waitBeteweenEffectAndSpawn);

            GameObject unitInstance = Instantiate(unit, spawnTransform.position, unit.transform.rotation);
            unitInstance.transform.SetParent(spawnerHolder.transform);

            yield return new WaitForSeconds(waitBetweenSpawn);
        }
    }
}