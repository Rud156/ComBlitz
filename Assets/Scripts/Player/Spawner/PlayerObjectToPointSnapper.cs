using ComBlitz.Extensions;
using UnityEngine;

namespace ComBlitz.Player.Spawner
{
    public class PlayerObjectToPointSnapper : MonoBehaviour
    {
        public Transform groundTrackingPoint;
        public Transform objectSnappingPoint;

        private void Update()
        {
            float xPos = ExtensionFunctions.GetClosestMultiple(groundTrackingPoint.position.x);
            float zPos = ExtensionFunctions.GetClosestMultiple(groundTrackingPoint.position.z);

            objectSnappingPoint.transform.position = new Vector3(xPos, 0, zPos);
        }
    }
}