using ComBlitz.ConstantData;
using EZCameraShake;
using UnityEngine;

namespace ComBlitz.StoryScene
{
    public class StopSmokeOnContactGround : MonoBehaviour
    {
        public GameObject rocketSmoke;
        public GameObject explosionSmoke;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(TagManager.MainGround))
            {
                Instantiate(explosionSmoke, transform.position, Quaternion.identity);
                rocketSmoke.SetActive(false);
                CameraShaker.Instance.ShakeOnce(5, 5, 0.3f, 2);
            }
        }
    }
}