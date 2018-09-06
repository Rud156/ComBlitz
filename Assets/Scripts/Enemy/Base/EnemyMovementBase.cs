using DeBomb.ConstantData;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace DeBomb.Enemy
{
    public abstract class EnemyMovementBase : MonoBehaviour
    {
        public float maxPLayerTargetDistance;

        private NavMeshAgent enemyAgent;

        private Transform playerTransform;
        private Transform baseTransform;
        private Transform[] constructionPlatforms;

        private void Start()
        {
            enemyAgent = GetComponent<NavMeshAgent>();

            playerTransform = GameObject.FindGameObjectWithTag(TagManager.Player).transform;
            baseTransform = GameObject.FindGameObjectWithTag(TagManager.Base).transform;
            constructionPlatforms = GameObject.FindGameObjectsWithTag(TagManager.Platform)
                .Select(_ => _.transform).ToArray();
        }

        private void Update()
        {
            CheckPlayerDistance();
        }

        private void CheckPlayerDistance()
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < maxPLayerTargetDistance)
                enemyAgent.SetDestination(playerTransform.position);
            else
            {
                if(constructionPlatforms.Length > 0)
                {

                }
            }

        }

        public void AttackObject()
        {
        }
    }
}