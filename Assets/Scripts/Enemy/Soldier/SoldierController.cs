using DeBomb.Enemy.Base;

namespace DeBomb.Enemy.Soldier
{
    public class SoldierController : EnemyControllerBase
    {
        // Use this for initialization
        private void Start()
        {
            base.enemyAttackAnimParam = "SoldierAttacking";
            base.enemyMoveAnimParam = "SoldierMoving";

            base.Init();
        }

        // Update is called once per frame
        private void Update() => base.UpdateEnemy();
    }
}