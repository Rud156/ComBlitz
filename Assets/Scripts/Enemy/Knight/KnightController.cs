using ComBlitz.Enemy.Base;
using UnityEngine;

namespace ComBlitz.Enemy.Knight
{
    public class KnightController : EnemyControllerBase
    {
        [Header("Sword")] public BoxCollider swordContactPoint;

        private void Start()
        {
            base.enemyAttackAnimParam = "KnightAttacking";
            base.enemyMoveAnimParam = "KnightMoving";

            base.Init();

            swordContactPoint.enabled = false;
        }

        private void Update() => base.UpdateEnemy();

        public void EnableSwordContact() => swordContactPoint.enabled = true;

        public void DisableSwordContact() => swordContactPoint.enabled = false;
    }
}