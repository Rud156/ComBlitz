using DeBomb.Player.Data;
using UnityEngine;

namespace DeBomb.Player.Shooter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerShootController : MonoBehaviour
    {
        public GameObject bullet;
        public float bulletLaunchVelocity;
        public Transform bulletLaunchPoint;

        private Rigidbody playerRB;
        private Animator playerAnimator;

        // Use this for initialization
        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        private void Update() => PlayerShoot();

        private void PlayerShoot()
        {
            if (Input.GetMouseButton(0))
            {
                playerAnimator.SetBool(PlayerContantData.PlayerShootAnimParam, true);
                playerRB.velocity = Vector3.zero + Vector3.up * playerRB.velocity.y;
            }
            else
                playerAnimator.SetBool(PlayerContantData.PlayerShootAnimParam, false);
        }

        private void ShootBullet()
        {
            GameObject bulletInstance = Instantiate(bullet, bulletLaunchPoint.position, Quaternion.identity);
            bulletInstance.GetComponent<Rigidbody>().velocity = bulletLaunchVelocity * transform.forward;
        }
    }
}