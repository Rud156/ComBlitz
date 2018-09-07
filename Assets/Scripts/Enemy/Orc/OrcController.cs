using DeBomb.Enemy.Base;

namespace DeBomb.Enemy.Orc
{
    public class OrcController : EnemyControllerBase
    {
        private void Start()
        {
            base.enemyAttackAnimParam = "OrcAttacking";
            base.enemyMoveAnimParam = "OrcMoving";

            base.Init();
        }

        private void Update() => base.UpdateEnemy();
    }
}