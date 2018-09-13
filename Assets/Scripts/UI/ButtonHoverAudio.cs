using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComBlitz.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonHoverAudio : MonoBehaviour
    {
        private AudioSource audioSource;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start() => audioSource = GetComponent<AudioSource>();

        public void OnPointerEnter() => audioSource.Play();
    }
}