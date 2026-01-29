using UnityEngine;
using TMPro;

public class PanelTextController : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text textToChange;

    [TextArea(1, 10)]
    [SerializeField] private string textForMethodNa = "Текст метода 2";
    [TextArea(1, 10)]
    [SerializeField] private string textForMethodK = "Текст метода 3";
    [TextArea(1, 10)]
    [SerializeField] private string textForMethodLi = "Текст метода 4";

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void ChangeTextNa()
    {
        ChangeText(textForMethodNa);
    }

    public void ChangeTextToMethodK()
    {
        ChangeText(textForMethodK);
    }

    public void ChangeTextToMethodLi()
    {
        ChangeText(textForMethodLi);
    }

    private void ChangeText(string newText)
    {
        textToChange.text = newText;
    }
}