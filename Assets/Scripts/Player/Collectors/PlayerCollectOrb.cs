using ComBlitz.Resources;
using ComBlitz.ConstantData;
using UnityEngine;

namespace ComBlitz.Player.Collectors
{
    public class PlayerCollectOrb : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagManager.GreenOrb))
            {
                ResourceManager.instance.AddOrb(ResourceManager.OrbType.Green, 1);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag(TagManager.RedOrb))
            {
                ResourceManager.instance.AddOrb(ResourceManager.OrbType.Red, 1);
                Destroy(other.gameObject);
            }
            else if (other.CompareTag(TagManager.YellowOrb))
            {
                ResourceManager.instance.AddOrb(ResourceManager.OrbType.Orange, 1);
                Destroy(other.gameObject);
            }
        }
    }
}