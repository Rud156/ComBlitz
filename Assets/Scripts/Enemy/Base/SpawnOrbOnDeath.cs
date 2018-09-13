using ComBlitz.Scene;
using UnityEngine;

namespace ComBlitz.Enemy.Base
{
    public class SpawnOrbOnDeath : MonoBehaviour
    {
        public GameObject orbPrefab;
        public int minSpawnCount;
        public int maxSpawnCount;

        public void SpawnOrb()
        {
            int randomNumber = Random.Range(0, 1000);
            int randomValue = randomNumber % maxSpawnCount + minSpawnCount;

            for (int i = 0; i < randomValue; i++)
            {
                Vector3 randomPointInSphere = Random.insideUnitSphere;
                float randomY = Random.Range(0.3f, 1);
                Vector3 updatedPoint = new Vector3(randomPointInSphere.x, randomY, randomPointInSphere.z);

                Instantiate(orbPrefab, updatedPoint + transform.position, Quaternion.identity);
            }
            
            // Add Kill When Enemy Dead
            ScoreManager.instance.AddKill();
        }
    }
}