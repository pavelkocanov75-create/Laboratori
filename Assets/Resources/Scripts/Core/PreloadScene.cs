using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARLaboratory.Core
{
    public class PreloadScene : MonoBehaviour
    {
        private AsyncOperation _asyncOperation;

        private IEnumerator LoadSceneAsyncProcess()
        {
            _asyncOperation = SceneManager.LoadSceneAsync(1);

            _asyncOperation.allowSceneActivation = false;

            while (_asyncOperation.isDone)
            {
                Debug.Log($"[scene]: [load progress]: {_asyncOperation.progress}");
                yield return null;
            }
        }

        private void Start()
        {
            Debug.Log("Started Scene Preloading");
            StartCoroutine(LoadSceneAsyncProcess()); 
        }

        public void ActivateLaboratoryScene()
        {
            _asyncOperation.allowSceneActivation = true;
        }
    }
}