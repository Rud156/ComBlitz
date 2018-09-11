using ComBlitz.Player.Data;
using UnityEngine;

namespace ComBlitz.Player.Shooter
{
    [RequireComponent(typeof(Animator))]
    public class PlayerShootController : MonoBehaviour
    {
        public GameObject bullet;
        public float bulletLaunchVelocity;
        public Transform bulletLaunchPoint;

        private Animator playerAnimator;
        private bool stopShooting;

        // Use this for initialization
        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            stopShooting = false;
        }

        // Update is called once per frame
        private void Update() => PlayerShoot();

        public void ActivateShooting() => stopShooting = false;

        public void DeActivateShooting() => stopShooting = true;

        private void PlayerShoot()
        {
            if (stopShooting)
                return;


            if (Input.GetMouseButton(0))
                playerAnimator.SetBool(PlayerContantData.PlayerShootAnimParam, true);
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