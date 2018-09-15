using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComBlitz.Extensions
{
    public class Rotator : MonoBehaviour
    {
        public enum Direction
        {
            xAxis,
            yAxis,
            zAxis
        };

        public Direction direction;
        public float rotationSpeed = 5f;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            switch (direction)
            {
                case Direction.xAxis:
                    transform.Rotate(Vector3.right * rotationSpeed);
                    break;

                case Direction.yAxis:
                    transform.Rotate(Vector3.up * rotationSpeed);
                    break;

                case Direction.zAxis:
                    transform.Rotate(Vector3.forward * rotationSpeed);
                    break;
            }
        }
    }
}