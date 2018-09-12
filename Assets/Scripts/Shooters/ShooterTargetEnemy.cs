using System.Collections;
using System.Collections.Generic;
using ComBlitz.ConstantData;
using UnityEngine;


namespace ComBlitz.Shooters
{
    public class ShooterTargetEnemy : MonoBehaviour
    {
        [Header(("Shooter Stats"))] public float maxDistanceToTarget;
        public float minDistanceToTarget;
        public float fireRate;
        public Transform projectileShooter;
        public GameObject projectileShotEffect;

        [Header(("Bullet Stats"))] public GameObject projectile;
        public Transform projectileLaunchPoint;
        public float launchSpeed;

        [Header("Debug")] public bool playOnStart;

        private Transform enemyHolder;

        private bool movementStarted;
        private GameObject currentTarget;
        private Quaternion targetLookRotation;

        private void Start()
        {
            enemyHolder = GameObject.FindGameObjectWithTag(TagManager.EnemyHolder).transform;
            movementStarted = false;
            targetLookRotation = Quaternion.identity;

            if (playOnStart)
                StartShooting();
        }

        private void Update()
        {
            if (!movementStarted || currentTarget == null)
                return;

            Vector3 direction = currentTarget.transform.position -
                                projectileLaunchPoint.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            targetLookRotation = Quaternion.Slerp(projectileLaunchPoint.rotation, lookRotation, 5 * Time.deltaTime);
            projectileShooter.rotation = targetLookRotation;
        }

        public void StartShooting() => StartCoroutine(FindAndShootEnemy());

        private GameObject GetNearestEnemy()
        {
            GameObject nearestEnemy = null;
            int childCount = enemyHolder.childCount;
            float maxDistance = maxDistanceToTarget;
            
            for (int i = 0; i < childCount; i++)
            {
                GameObject enemy = enemyHolder.GetChild(i).gameObject;
                float enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);

                if (enemyDistance <= maxDistance && enemyDistance >= minDistanceToTarget)
                {
                    maxDistance = enemyDistance;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }

        private IEnumerator FindAndShootEnemy()
        {
            movementStarted = true;

            while (true)
            {
                currentTarget = GetNearestEnemy();

                if (currentTarget != null)
                {
//                    GameObject shotEffectInstance = Instantiate(projectileShotEffect,
//                        projectileLaunchPoint.position, Quaternion.identity);
//                    shotEffectInstance.transform.rotation = targetLookRotation;
//
//                    GameObject projectileInstance = Instantiate(projectile,
//                        projectileLaunchPoint.position, Quaternion.identity);
//                    projectileInstance.GetComponent<Rigidbody>().velocity =
//                        projectileLaunchPoint.forward * launchSpeed;
                }

                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}