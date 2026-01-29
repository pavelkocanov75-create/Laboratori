using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace ARLaboratory.Core
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] private AbstractLoadable[] _loadables;
    
        [SerializeField] private UnityEvent OnLoadingFinished;
    
        private void Update()
        {
            if (IsFinishedLoading())
                OnLoadingFinished?.Invoke();
        }
    
        private bool IsFinishedLoading() => _loadables.All(x => x.IsDone());
    }
}
