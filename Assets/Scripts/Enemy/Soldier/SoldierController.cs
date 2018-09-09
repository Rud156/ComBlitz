using ComBlitz.Enemy.Base;
using UnityEngine;

namespace ComBlitz.Enemy.Soldier
{
    public class SoldierController : EnemyControllerBase
    {
        [Header("Soldier BUllet")]
        public GameObject bullet;

        public float bulletLaunchVelocity;
        public Transform bulletLaunchPoint;
        public float minAttackDistance;

        // Use this for initialization
        private void Start()
        {
            base.enemyAttackAnimParam = "SoldierShooting";
            base.enemyMoveAnimParam = "SoldierMoving";

            base.Init();
        }

        // Update is called once per frame
        private void Update() => base.UpdateEnemy();

        protected override void MoveTowardsTargetAndAttack()
        {
            if (base.currentTarget != null)
            {
                base.enemyAgent.SetDestination(currentTarget.position);

                if (Vector3.Distance(transform.position, currentTarget.position) <= minAttackDistance)
                    base.enemyAnimator.SetBool(base.enemyAttackAnimParam, true);
                else
                    base.enemyAnimator.SetBool(base.enemyAttackAnimParam, false);
            }
            else
            {
                base.enemyAnimator.SetBool(base.enemyAttackAnimParam, false);
                base.enemyAgent.ResetPath();
            }
        }

        public void ShootBullet()
        {
            GameObject bulletInstance = Instantiate(bullet, bulletLaunchPoint.position, Quaternion.identity);
            bulletInstance.GetComponent<Rigidbody>().velocity = bulletLaunchVelocity * transform.forward;
        }
    }
}