using DeBomb.Player.Data;
using UnityEngine;

namespace DeBomb.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMoveController : MonoBehaviour
    {
        public float movementSpeed = 100f;
        public float rotationSpeed = 50f;

        private Animator playerAnimator;
        private Rigidbody playerRB;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update
        /// methods is called the first time.
        /// </summary>
        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            MovePlayerVerticalAndHorizontal();

            SetAndLimitPlayerAnimation();

            PointPlayerTowardsMouse();
        }

        private void MovePlayerVerticalAndHorizontal()
        {
            float moveZ = Input.GetAxis(PlayerContantData.VerticalAxis);
            float moveX = Input.GetAxis(PlayerContantData.HorizontalAxis);

            Vector3 zVelocity = Vector3.zero;
            Vector3 xVelocity = Vector3.zero;

            if (moveZ != 0)
                zVelocity = transform.forward * moveZ;

            if (moveX != 0)
                xVelocity = transform.right * moveX;

            Vector3 combinedVelocity = (zVelocity + xVelocity) * movementSpeed * Time.deltaTime;
            playerRB.velocity = new Vector3(combinedVelocity.x, playerRB.velocity.y, combinedVelocity.z);
        }

        private void SetAndLimitPlayerAnimation()
        {
            float moveZ = Input.GetAxis(PlayerContantData.VerticalAxis);
            float moveX = Input.GetAxis(PlayerContantData.HorizontalAxis);

            playerAnimator.SetFloat(PlayerContantData.PlayerVerticalMovement, moveZ);
            playerAnimator.SetFloat(PlayerContantData.PlayerHorizontalMovement, moveX);
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
    }
}