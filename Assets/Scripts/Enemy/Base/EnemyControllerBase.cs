using ComBlitz.ConstantData;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ComBlitz.Enemy.Base
{
    public abstract class EnemyControllerBase : MonoBehaviour
    {
        [Header("Other Target Stats")]
        public float maxTargetSwitchDistance = 10f;

        public float minPlayerTargetDistance = 10f;

        [Header("Enemy Control Stats")]
        public float enemyMovementThreshold = 1f;

        public float waitBetweenAttackTime = 2f;

        protected NavMeshAgent enemyAgent;
        protected Animator enemyAnimator;

        private Transform playerTransform;
        private Transform baseTransform;
        private Transform shooterHolder;

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
            shooterHolder = GameObject.FindGameObjectWithTag(TagManager.ShooterHolder).transform;

            currentTarget = baseTransform;
            enemyAttackPlaying = false;
        }

        protected void UpdateEnemy()
        {
            ChangeTargetIfInPath();
            ChangeTargetToPlayerIfNear();

            MoveTowardsTargetAndAttack();
            SetMovementAnimation();
        }

        protected virtual void MoveTowardsTargetAndAttack()
        {
            if (enemyAttackPlaying)
                enemyAgent.ResetPath();
            else
                enemyAgent.SetDestination(currentTarget.position);

            if (!enemyAgent.pathPending && !enemyAttackPlaying)
            {
                if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    if (!enemyAgent.hasPath || enemyAgent.velocity.sqrMagnitude == 0f)
                        StartCoroutine(AttackPlayer());
                }
            }
        }

        private void SetMovementAnimation()
        {
            if (enemyAgent.velocity.magnitude > enemyMovementThreshold)
                enemyAnimator.SetBool(enemyMoveAnimParam, true);
            else
                enemyAnimator.SetBool(enemyMoveAnimParam, false);
        }

        private void ChangeTargetIfInPath()
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

        private void ChangeTargetToPlayerIfNear()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= minPlayerTargetDistance)
                currentTarget = playerTransform;
        }

        private IEnumerator AttackPlayer()
        {
            enemyAnimator.SetBool(enemyAttackAnimParam, true);
            enemyAttackPlaying = true;

            yield return new WaitForSeconds(waitBetweenAttackTime);

            enemyAnimator.SetBool(enemyAttackAnimParam, false);
            enemyAttackPlaying = false;
        }
    }
}