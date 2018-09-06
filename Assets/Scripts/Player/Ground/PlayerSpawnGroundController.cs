using DeBomb.Extensions;
using DeBomb.Ground;
using UnityEngine;

namespace DeBomb.Player.Ground
{
    public class PlayerSpawnGroundController : MonoBehaviour
    {
        public Transform groundTracker;
        public float overlapSphereRadius = 1f;

        private void Start() => GroundManager.instance.createGroundInstance += CreateGroundInWorld;

        private void OnDestroy() => GroundManager.instance.createGroundInstance -= CreateGroundInWorld;

        private void CreateGroundInWorld(GameObject ground)
        {
            Collider[] colliders = Physics.OverlapSphere(groundTracker.position, overlapSphereRadius);

            bool clear = false;
            float xPos = 0;
            float zPos = 0;

            if (colliders.Length == 0)
            {
                xPos = ExtensionFunctions.GetClosestMultiple(groundTracker.position.x);
                zPos = ExtensionFunctions.GetClosestMultiple(groundTracker.position.z);

                clear = true;
            }

            if (clear)
            {
                ground.transform.position = new Vector3(xPos, 0, zPos);
            }
        }
    }
}