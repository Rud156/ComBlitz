using DeBomb.ConstantData;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace DeBomb.Enemy
{
    public abstract class EnemyMovementBase : MonoBehaviour
    {
        public float maxPLayerTargetDistance;

        private NavMeshAgent enemyAgent;

        private Transform playerTransform;

        private void Start()
        {
            enemyAgent = GetComponent<NavMeshAgent>();
            playerTransform = GameObject.FindGameObjectWithTag(TagManager.Player).transform;
        }

        private void Update() => MoveTowardsPlayerAndAttack();

        private void MoveTowardsPlayerAndAttack()
        {
            if (!enemyAgent.pathPending)
            {
                if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    if (!enemyAgent.hasPath || enemyAgent.velocity.sqrMagnitude == 0f)
                        StartCoroutine(AttackPlayer());
                }
            }

            enemyAgent.SetDestination(playerTransform.position);
        }

        protected abstract IEnumerator AttackPlayer();
    }
}