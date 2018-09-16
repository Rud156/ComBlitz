using ComBlitz.Extensions;
using UnityEngine;

namespace ComBlitz.Player.Spawner
{
    public class PointToGridCameraSnapper : MonoBehaviour
    {
        public Transform groundTrackingPoint;
        public Transform objectSnappingPoint;

        private void Update()
        {
            Plane groundTrackerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;

            if (groundTrackerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                groundTrackingPoint.transform.position = targetPoint;
            }
        }

        private void LateUpdate()
        {
            float xPos = ExtensionFunctions.GetClosestMultiple(groundTrackingPoint.position.x);
            float zPos = ExtensionFunctions.GetClosestMultiple(groundTrackingPoint.position.z);

            objectSnappingPoint.transform.position = new Vector3(xPos, 0, zPos);
        }
    }
}