using UnityEngine;

namespace ComBlitz.Scene.StoryScene
{
    public class SkipSceneClick : MonoBehaviour
    {
        public void SkipStoryScene() => StorySceneManager.instance.SkipScene();
    }
}