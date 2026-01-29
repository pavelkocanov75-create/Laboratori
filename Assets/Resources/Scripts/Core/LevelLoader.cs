using System.Collections;
using UnityEngine;

namespace ARLaboratory.Core
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator CrossfadeAnimator;

        private readonly WaitForSeconds _waitForTransition = new(1f);

        public void LoadNextScene(int levelIndex)
        {
            StartCoroutine(LoadScene(levelIndex));
        }

        private IEnumerator LoadScene(int levelIndex)
        {
            CrossfadeAnimator.SetTrigger("Start");

            yield return _waitForTransition;

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelIndex);
        }
    }
}
