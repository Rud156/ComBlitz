using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ComBlitz.Scene.StoryScene
{
    public class DialogueManager : MonoBehaviour
    {
        #region Singleton

        public static DialogueManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (instance != this)
                Destroy(gameObject);
        }

        #endregion Singleton

        public delegate void DialogueComplete();

        public DialogueComplete dialogueComplete;

        [Header("Dialogue")] public Text dialogueText;
        [TextArea] public string[] dialogues;
        public float waitTimeBewteenDialogues;

        public void StartDialogues() => StartCoroutine(DisplayDialogues());

        private IEnumerator DisplayDialogues()
        {
            foreach (string dialogue in dialogues)
            {
                dialogueText.text = dialogue;
                yield return new WaitForSeconds(waitTimeBewteenDialogues);
            }
            
            dialogueComplete?.Invoke();
        }
    }
}