using DeBomb.ConstantData;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace DeBomb.Enemy.Base
{
    public abstract class EnemyMovementBase : MonoBehaviour
    {
        [Header("Other Target Stats")]
        public float maxTargetSwitchDistance = 10f;

        public float minPlayerTargetDistance = 10f;
        public Transform shooterHolder;

        [Header("Enemy Control Stats")]
        public float enemyMovementThreshold = 1f;

        public float waitBetweenAttackTime = 2f;

        protected NavMeshAgent enemyAgent;
        protected Animator enemyAnimator;

        protected Transform playerTransform;
        protected Transform baseTransform;
        protected bool enemyAttackPlaying;

        protected string enemyMoveAnimParam;
        protected string enemyAttackAnimParam;

        protected Transform currentTarget;

        protected void Init()
        {
            enemyAnimator = GetComponent<Animator>();
            enemyAgent = GetComponent<NavMeshAgent>();

            playerTransform = GameObject.FindGameObjectWithTag(TagManager.Player).transform;
            baseTransform = GameObject.FindGameObjectWithTag(TagManager.Base).transform;

            currentTarget = baseTransform;
            enemyAttackPlaying = false;
        }

        protected void MoveTowardsTargetAndAttack()
        {
            if (!enemyAgent.pathPending && !enemyAttackPlaying)
            {
                if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    if (!enemyAgent.hasPath || enemyAgent.velocity.sqrMagnitude == 0f)
                        StartCoroutine(AttackPlayer());
                }
            }

            if (enemyAttackPlaying)
                enemyAgent.ResetPath();
            else
                enemyAgent.SetDestination(currentTarget.position);
        }

        protected void ChangeTargetIfInPath()
        {
            int shootersChildCount = shooterHolder.transform.childCount;
            float shortestDistance = maxTargetSwitchDistance;
            Transform potentialTarget = null;

            for (int i = 0; i < shootersChildCount; i++)
            {
                float distanceToShooter = Vector3.Distance(transform.position,
                    shooterHolder.transform.GetChild(i).position);

                if (distanceToShooter <= shortestDistance)
                {
                    shortestDistance = distanceToShooter;
                    potentialTarget = shooterHolder.transform.GetChild(i);
                }
            }

            if (potentialTarget == null)
                potentialTarget = baseTransform;

            currentTarget = potentialTarget;
        }

        protected void ChangeTargetToPlayerIfNear()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= minPlayerTargetDistance)
                currentTarget = playerTransform;
        }

        protected abstract IEnumerator AttackPlayer();
    }
}