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
            base.ChangeTargetToPlayerIfNear();
            base.MoveTowardsTargetAndAttack();

            SetMovementAnimation();
        }

        private void SetMovementAnimation()
        {
            if (base.enemyAgent.velocity.magnitude > base.enemyMovementThreshold)
                base.enemyAnimator.SetBool(base.enemyMoveAnimParam, true);
        }

        protected override IEnumerator AttackPlayer()
        {
            base.enemyAnimator.SetBool(base.enemyAttackAnimParam, true);
            base.enemyAttackPlaying = true;
            yield return new WaitForSeconds(base.waitBetweenAttackTime);

            base.enemyAttackPlaying = false;
        }
    }
}