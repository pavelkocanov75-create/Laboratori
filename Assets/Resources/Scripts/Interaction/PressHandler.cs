using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ARLaboratory.Interaction
{
	public class PressHandler : MonoBehaviour {
		[SerializeField] private UnityEvent OnPress;

		private void OnMouseUpAsButton()
		{
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			OnPress?.Invoke();
		}
	}
}