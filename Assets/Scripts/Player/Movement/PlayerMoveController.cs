using ComBlitz.Extensions;
using ComBlitz.Player.Data;
using UnityEngine;

namespace ComBlitz.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMoveController : MonoBehaviour
    {
        public float movementSpeed = 100f;
        public float rotationSpeed = 50f;

        private Animator playerAnimator;
        private Rigidbody playerRB;
        private bool stopMovement;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update
        /// methods is called the first time.
        /// </summary>
        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            playerRB = GetComponent<Rigidbody>();

            stopMovement = false;
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (stopMovement)
                return;

            MovePlayerVerticalAndHorizontal();

            SetAndLimitPlayerAnimation();

            PointPlayerTowardsMouse();
        }

        public void ActivateMovement() => stopMovement = false;

        public void DeActivateMovement() => stopMovement = true;

        private void MovePlayerVerticalAndHorizontal()
        {
            float moveZ = Input.GetAxis(PlayerContantData.VerticalAxis);
            float moveX = Input.GetAxis(PlayerContantData.HorizontalAxis);

            Vector3 zVelocity = Vector3.zero;
            Vector3 xVelocity = Vector3.zero;

            if (moveZ != 0)
                zVelocity = Vector3.forward * moveZ;

            if (moveX != 0)
                xVelocity = Vector3.right * moveX;

            Vector3 combinedVelocity = (zVelocity + xVelocity) * movementSpeed * Time.deltaTime;
            playerRB.velocity = new Vector3(combinedVelocity.x, playerRB.velocity.y, combinedVelocity.z);
        }

        private void SetAndLimitPlayerAnimation()
        {
            float moveZ = Input.GetAxis(PlayerContantData.VerticalAxis);
            float moveX = Input.GetAxis(PlayerContantData.HorizontalAxis);

            float yRotation = ExtensionFunctions.To360Angle(transform.rotation.eulerAngles.y);

            float vAxisVMovement = moveZ * Mathf.Cos(Mathf.Deg2Rad * yRotation);
            float vAxisHMovement = moveZ * Mathf.Sin(Mathf.Deg2Rad * yRotation);

            float hAxisVMovement = moveX * Mathf.Sin(Mathf.Deg2Rad * yRotation);
            float hAxisHMovement = moveX * Mathf.Cos(Mathf.Deg2Rad * yRotation);

            float vMovement = vAxisVMovement + hAxisVMovement;
            float hMovement = vAxisHMovement + hAxisHMovement;

            playerAnimator.SetFloat(PlayerContantData.PlayerVerticalMovement, vMovement);
            playerAnimator.SetFloat(PlayerContantData.PlayerHorizontalMovement, hMovement);
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