using ComBlitz.Enemy.Base;
using UnityEngine;

namespace ComBlitz.Enemy.Orc
{
    public class OrcController : EnemyControllerBase
    {
        [Header("Orc Axe")]
        public BoxCollider orcAxeContactPoint;

        private void Start()
        {
            base.enemyAttackAnimParam = "OrcAttacking";
            base.enemyMoveAnimParam = "OrcMoving";

            base.Init();
        }

        private void Update()
        {
            if (base.enemyAttackPlaying)
                orcAxeContactPoint.enabled = true;
            else
                orcAxeContactPoint.enabled = false;

            base.UpdateEnemy();
        }
    }
}