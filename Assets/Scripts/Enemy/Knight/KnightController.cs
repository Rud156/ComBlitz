using DeBomb.Enemy.Base;

namespace DeBomb.Enemy.Knight
{
    public class KnightController : EnemyControllerBase
    {
        private void Start()
        {
            base.enemyAttackAnimParam = "KnightAttacking";
            base.enemyMoveAnimParam = "KinghtMoving";

            base.Init();
        }

        private void Update() => base.UpdateEnemy();
    }
}