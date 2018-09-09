using UnityEngine;

namespace ComBlitz.Extensions
{
    public class DestroyOnContact : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other) => Destroy(gameObject);
    }
}