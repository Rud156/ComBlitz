using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCamera
{
    public class CameraFollowTarget : MonoBehaviour
    {
        [Header("Base Stats")]
        public float followSpeed = 10f;
        public float lookAtSpeed = 10f;
        public bool useRotation = false;
        public Vector3 cameraOffset;

        [Header("Target")]
        public Transform target;

        private Vector3 lastTargetPosition;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start() => lastTargetPosition = target.position;

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        void FixedUpdate()
        {
            UpdateLastTargetPosition();

            if (useRotation)
                LootAtTarget();
            MoveToTarget();
        }

        private void LootAtTarget()
        {
            Vector3 targetPosition = target != null ? target.position : lastTargetPosition;

            Vector3 lookDirection = targetPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookAtSpeed * Time.deltaTime);
        }

        private void MoveToTarget()
        {
            Vector3 targetVector = lastTargetPosition;
            Vector3 forwardVector = Vector3.forward;
            Vector3 rightVector = Vector3.right;
            Vector3 upVector = Vector3.up;

            Vector3 targetPosition = targetVector +
                forwardVector * cameraOffset.z +
                rightVector * cameraOffset.x +
                upVector * cameraOffset.y;

            transform.position = Vector3.Lerp(transform.position, targetPosition,
                followSpeed * Time.deltaTime);
        }

        private void UpdateLastTargetPosition()
        {
            if (target != null)
                lastTargetPosition = target.position;
        }
    }
}
