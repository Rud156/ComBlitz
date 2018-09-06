using DeBomb.Extensions;
using DeBomb.Ground;
using UnityEngine;

namespace DeBomb.Player.Ground
{
    public class PlayerSpawnGroundController : MonoBehaviour
    {
        public Transform groundTracker;
        public float overlapSphereRadius = 2f;

        [Header("Grounds")]
        public GameObject grassGround;

        public GameObject lavaGround;
        public GameObject dirtGround;

        // Update is called once per frame
        private void Update() => CheckGroundClearAndPlace();

        private void CheckGroundClearAndPlace()
        {
            if (!Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.X) && !Input.GetKeyDown(KeyCode.C))
                return;

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
                PlaceGround(xPos, zPos);
        }

        private void PlaceGround(float xPos, float zPos)
        {
            GameObject instantiatedGround = null;

            if (Input.GetKeyDown(KeyCode.Z))
                instantiatedGround = Instantiate(grassGround, new Vector3(xPos, 0, zPos), Quaternion.identity);
            else if (Input.GetKeyDown(KeyCode.X))
                instantiatedGround = Instantiate(lavaGround, new Vector3(xPos, 0, zPos), Quaternion.identity);
            else if (Input.GetKeyDown(KeyCode.C))
                instantiatedGround = Instantiate(dirtGround, new Vector3(xPos, 0, zPos), Quaternion.identity);

            if (instantiatedGround != null)
                GroundFall.instance.AddGround(instantiatedGround);
        }
    }
}