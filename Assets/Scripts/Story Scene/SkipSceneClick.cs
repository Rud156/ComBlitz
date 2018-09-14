using UnityEngine;

namespace ComBlitz.StoryScene
{
    public class SkipSceneClick : MonoBehaviour
    {
        public void SkipStoryScene() => StorySceneManager.instance.SkipScene();
    }
}