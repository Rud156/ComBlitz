using System.Collections;
using System.Collections.Generic;
using ComBlitz.Extensions;
using ComBlitz.Scene.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComBlitz.StoryScene
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue")] public Text dialogueText;
        [TextArea] public string[] dialogues;
        public float waitTimeBewteenDialogues;

        private void Start() => StartCoroutine(DisplayDialogues());

        private IEnumerator DisplayDialogues()
        {
            foreach (string dialogue in dialogues)
            {
                dialogueText.text = dialogue;
                yield return new WaitForSeconds(waitTimeBewteenDialogues);
            }

            Fader.instance.ActivateFading();
        }
    }
}