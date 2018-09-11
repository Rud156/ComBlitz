using System.Collections;
using System.Collections.Generic;
using ComBlitz.ConstantData;
using UnityEngine;


namespace ComBlitz.Shooters
{
    public class ShooterTargetEnemy : MonoBehaviour
    {
        [Header(("Shooter Stats"))] public float maxDistanceToTarget;
        public float fireRate;
        public Transform projectileShooter;
        public GameObject projectileShotEffect;

        [Header(("Bullet Stats"))] public GameObject projectile;
        public Transform projectileLaunchPoint;
        public float launchSpeed;

        private Transform enemyHolder;

        private void Start() => enemyHolder = GameObject.FindGameObjectWithTag(TagManager.EnemyHolder).transform;

        public void StartShooting() => StartCoroutine(FindAndShootEnemy());

        private GameObject GetNearestEnemy()
        {
            GameObject nearestEnemy = null;
            int childCount = enemyHolder.childCount;
            float minDistance = maxDistanceToTarget;

            for (int i = 0; i < childCount; i++)
            {
                GameObject enemy = enemyHolder.GetChild(i).gameObject;
                float enemyDistance = Vector3.Distance(enemy.transform.position, transform.position);

                if (enemyDistance <= minDistance)
                {
                    minDistance = enemyDistance;
                    nearestEnemy = enemy;
                }
            }

            return nearestEnemy;
        }

        private IEnumerator FindAndShootEnemy()
        {
            while (true)
            {
                GameObject nearestEnemy = GetNearestEnemy();

                if (nearestEnemy != null)
                {
                    Vector3 direction = nearestEnemy.transform.position -
                                        projectileLaunchPoint.transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);

                    projectileShooter.transform.rotation = lookRotation;

                    GameObject shotEffectInstance = Instantiate(projectileShotEffect,
                        projectileLaunchPoint.transform.position, Quaternion.identity);
                    shotEffectInstance.transform.rotation = lookRotation;

                    GameObject projectileInstance = Instantiate(projectile,
                        projectileLaunchPoint.transform.position, Quaternion.identity);
                    projectileInstance.GetComponent<Rigidbody>().velocity =
                        projectileLaunchPoint.transform.forward * launchSpeed;
                }

                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}