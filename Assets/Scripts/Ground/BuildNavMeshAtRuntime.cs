using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ComBlitz.Ground
{
    public class BuildNavMeshAtRuntime :MonoBehaviour
    {
        public float navMeshRebuildRate = 0.5f;
        
        private NavMeshSurface navSurface;

        private void Start()
        {
            navSurface = GetComponent<NavMeshSurface>();
            StartCoroutine(BuildNavMesh());
        }

        private IEnumerator BuildNavMesh()
        {
            while (true)
            {
                Debug.Log("Building the NavMesh");
                navSurface.BuildNavMesh();
                yield return new WaitForSeconds(navMeshRebuildRate);
            }
        }
    }
}