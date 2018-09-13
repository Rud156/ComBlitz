using ComBlitz.ConstantData;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace ComBlitz.Enemy.Base
{
    public abstract class EnemyControllerBase : MonoBehaviour
    {
        [Header("Other Target Stats")] public float maxTargetSwitchDistance = 10f;
        public float minPlayerTargetDistance = 10f;

        [Header("Enemy Control Stats")] public float enemyMovementThreshold = 1f;
        public float waitBetweenAttackTime = 2f;

        [Header("Target Stop Distance")] public float baseDistance;
        public float playerDistance;
        public float bulletShooter;
        public float laserShooter;
        public float missileShooter;


        protected NavMeshAgent enemyAgent;
        protected Animator enemyAnimator;

        private Transform playerTransform;
        private Transform baseTransform;
        private Transform shooterHolder;

        private bool enemyAttackPlaying;
        protected string enemyMoveAnimParam;
        protected string enemyAttackAnimParam;

        protected Transform currentTarget;

        protected void Init()
        {
            enemyAnimator = GetComponent<Animator>();
            enemyAgent = GetComponent<NavMeshAgent>();

            playerTransform = GameObject.FindGameObjectWithTag(TagManager.Player)?.transform;
            baseTransform = GameObject.FindGameObjectWithTag(TagManager.Base)?.transform;
            shooterHolder = GameObject.FindGameObjectWithTag(TagManager.ShooterHolder).transform;

            currentTarget = baseTransform;
            enemyAttackPlaying = false;
        }

        protected void UpdateEnemy()
        {
            ChangeTargetIfInPath();
            ChangeTargetToPlayerIfNear();

            LookTowardsTarget();

            MoveTowardsTargetAndAttack();
            SetMovementAnimation();

            SetStoppingDistance();
        }

        protected virtual void MoveTowardsTargetAndAttack()
        {
            if (currentTarget == null)
            {
                enemyAgent.ResetPath();
                return;
            }

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

        private void SetStoppingDistance()
        {
            if (currentTarget == null)
                return;

            if (currentTarget.CompareTag(TagManager.Player))
                enemyAgent.stoppingDistance = playerDistance;
            else if (currentTarget.CompareTag(TagManager.Base))
                enemyAgent.stoppingDistance = baseDistance;
            else if (currentTarget.CompareTag(TagManager.BulletShooter))
                enemyAgent.stoppingDistance = bulletShooter;
            else if (currentTarget.CompareTag(TagManager.LaserShooter))
                enemyAgent.stoppingDistance = laserShooter;
            else if (currentTarget.CompareTag(TagManager.MissileShooter))
                enemyAgent.stoppingDistance = missileShooter;
        }

        private void SetMovementAnimation()
        {
            if (enemyAgent.velocity.magnitude > enemyMovementThreshold)
                enemyAnimator.SetBool(enemyMoveAnimParam, true);
            else
                enemyAnimator.SetBool(enemyMoveAnimParam, false);
        }

        private void LookTowardsTarget()
        {
            if (currentTarget == null)
                return;

            Vector3 lookDirection = currentTarget.position - transform.position;
            lookDirection.y = 0;

            Quaternion rotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
        }

        private void ChangeTargetIfInPath()
        {
            int shootersChildCount = shooterHolder.childCount;
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
            if(playerTransform == null)
                return;
            
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