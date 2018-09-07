using DeBomb.Enemy.Base;
using System.Collections;
using UnityEngine;

namespace DeBomb.Enemy.Knight
{
    public class KnightController : EnemyMovementBase
    {
        private void Start()
        {
            base.enemyAttackAnimParam = "KnightAttacking";
            base.enemyMoveAnimParam = "KinghtMoving";

            base.Init();
        }

        private void Update()
        {
            base.ChangeTargetIfInPath();
            base.MoveTowardsTargetAndAttack();

            SetMovementAnimation();
        }

        private void SetMovementAnimation()
        {
            if (enemyAgent.velocity.magnitude > enemyMovementThreshold)
                enemyAnimator.SetBool(enemyMoveAnimParam, true);
        }

        protected override IEnumerator AttackPlayer()
        {
            enemyAnimator.SetBool(enemyAttackAnimParam, true);
            enemyAttackPlaying = true;
            yield return new WaitForSeconds(waitBetweenAttackTime);

            enemyAttackPlaying = false;
        }
    }
}