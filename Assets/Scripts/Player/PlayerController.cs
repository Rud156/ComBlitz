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
            MovePlayer();

            PointPlayerTowardsMouse();
            PlayerShoot();
        }

        private void MovePlayer()
        {
            float moveZ = Input.GetAxis("Vertical");
            if (moveZ > 0)
            {
                playerAnimator.SetBool(playerAnimatorMove, true);

                Vector3 velocity = transform.forward * moveZ * movementSpeed * Time.deltaTime;
                playerRB.velocity = new Vector3(velocity.x, playerRB.velocity.y, velocity.z);
            }
            else
            {
                playerRB.velocity = Vector3.zero + Vector3.up * playerRB.velocity.y;
                playerAnimator.SetBool(playerAnimatorMove, false);
            }
        }

        private void PointPlayerTowardsMouse()
        {
            Vector3 mousePosition = Input.mousePosition;
            
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			mousePosition.z = transform.position.z;
            
			transform.LookAt(mousePosition);
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
