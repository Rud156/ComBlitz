using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeBomb.Ground
{
    public class GroundDestroyAll : MonoBehaviour
    {

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other) => Destroy(other.gameObject);

        /// <summary>
        /// OnCollisionEnter is called when this collider/rigidbody has begun
        /// touching another rigidbody/collider.
        /// </summary>
        /// <param name="other">The Collision data associated with this collision.</param>
        void OnCollisionEnter(Collision other) => Destroy(other.gameObject);
    }
}