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

        // Use this for initialization
        private void Start() => playerAnimator = GetComponent<Animator>();

        // Update is called once per frame
        private void Update() => PlayerShoot();

        private void PlayerShoot()
        {
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