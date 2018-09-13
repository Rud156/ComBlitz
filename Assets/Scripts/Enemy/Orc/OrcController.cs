using ComBlitz.Enemy.Base;
using UnityEngine;

namespace ComBlitz.Enemy.Orc
{
    public class OrcController : EnemyControllerBase
    {
        [Header("Orc Axe")] public BoxCollider orcAxeContactPoint;

        private void Start()
        {
            base.enemyAttackAnimParam = "OrcAttacking";
            base.enemyMoveAnimParam = "OrcMoving";

            base.Init();

            orcAxeContactPoint.enabled = false;
        }

        private void Update() => base.UpdateEnemy();

        public void EnableOrcAxeContact() => orcAxeContactPoint.enabled = true;

        public void DisableOrcAxeContact() => orcAxeContactPoint.enabled = false;
    }
}