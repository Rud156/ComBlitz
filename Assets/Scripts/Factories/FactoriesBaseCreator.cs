using System.Collections;
using UnityEngine;

namespace ComBlitz.Factories
{
    public abstract class FactoriesBaseCreator : MonoBehaviour
    {
        public GameObject unitCreated;
        public float timeBetweenSpawn;

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

        public abstract IEnumerator SpawnUnits();
    }
}