using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed = 100f;
        public float rotationSpeed = 50f;

        private Animator playerAnimator;
        private Rigidbody playerRB;

        private const string playerAnimatorMove = "PlayerMoving";
        private const string playerAnimatorShoot = "PlayerShooting";

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            playerAnimator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            MovePlayerVerticalAndHorizontal();

            SetAndLimitPlayerAnimation();

            PointPlayerTowardsMouse();
            PlayerShoot();
        }

        private void MovePlayerVerticalAndHorizontal()
        {
            float moveZ = Input.GetAxis("Vertical");
            float moveX = Input.GetAxis("Horizontal");

            Vector3 zVelocity = Vector3.zero;
            Vector3 xVelocity = Vector3.zero;

            if (moveZ > 0)
                zVelocity = transform.forward * moveZ;

            if (moveX != 0)
                xVelocity = transform.right * moveX;

            Vector3 combinedVelocity = (zVelocity + xVelocity) * movementSpeed * Time.deltaTime;
            playerRB.velocity = new Vector3(combinedVelocity.x, playerRB.velocity.y, combinedVelocity.z);
        }

        private void SetAndLimitPlayerAnimation()
        {
            float moveZ = Input.GetAxis("Vertical");
            float moveX = Input.GetAxis("Horizontal");

            if (moveZ > 0 || moveX != 0)
                playerAnimator.SetBool(playerAnimatorMove, true);
            else
                playerAnimator.SetBool(playerAnimatorMove, false);
        }

        private void PointPlayerTowardsMouse()
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;

            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                    rotationSpeed * Time.deltaTime);
            }
        }

        private void PlayerShoot()
        {
            if (Input.GetMouseButton(0))
            {
                playerRB.velocity = Vector3.zero + Vector3.up * playerRB.velocity.y;
                playerAnimator.SetBool(playerAnimatorShoot, true);
            }
            else
                playerAnimator.SetBool(playerAnimatorShoot, false);
        }
    }
}
