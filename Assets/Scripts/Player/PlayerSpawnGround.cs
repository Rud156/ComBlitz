using DeBomb.Extensions;
using UnityEngine;

namespace DeBomb.Player
{
    public class PlayerSpawnGround : MonoBehaviour
    {
        public Transform groundTracker;
        public float overlapSphereRadius = 2f;

        [Header("Grounds")]
        public GameObject grassGround;

        public GameObject lavaGround;
        public GameObject dirtGround;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                PlaceGroundOnPoint();
        }

        private void PlaceGroundOnPoint()
        {
            Collider[] colliders = Physics.OverlapSphere(groundTracker.position, overlapSphereRadius);
            
            if (colliders.Length == 0)
            {
                float xPos = ExtensionFunctions.GetClosestMultiple(groundTracker.position.x);
                float zPos = ExtensionFunctions.GetClosestMultiple(groundTracker.position.z);

                Instantiate(lavaGround, new Vector3(xPos, 0, zPos), Quaternion.identity);
            }
        }
    }
}