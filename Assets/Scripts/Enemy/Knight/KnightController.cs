using ComBlitz.Enemy.Base;
using UnityEngine;

namespace ComBlitz.Enemy.Knight
{
    public class KnightController : EnemyControllerBase
    {
        [Header("Sword")]
        public BoxCollider swordContactPoint;

        private void Start()
        {
            base.enemyAttackAnimParam = "KnightAttacking";
            base.enemyMoveAnimParam = "KnightMoving";

            base.Init();
        }

        private void Update()
        {
            if (base.enemyAttackPlaying)
                swordContactPoint.enabled = true;
            else
                swordContactPoint.enabled = false;

            base.UpdateEnemy();
        }
    }
}