using UnityEngine;
using TMPro;

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager Instance;
    
    public GameObject Keyboard;
    
    [HideInInspector]
    public TMP_InputField selectedInputField;

    private void Awake()
    {
        Instance = this;
    }

    public void DeleteLetter()
    {
        if (!selectedInputField)
        {
            return;
        }
        
        if(selectedInputField.text.Length != 0) {
            selectedInputField.text = selectedInputField.text.Remove(selectedInputField.text.Length - 1, 1);
        }
    }

    public void AddLetter(string letter)
    {
        selectedInputField.text = selectedInputField.text + letter;
    }
    
    public void ShowKeyboard(TMP_InputField text)
    {
        selectedInputField = text;
        Keyboard.SetActive(true);
    }
    
    public void HideKeyboard()
    {
        Keyboard.SetActive(false);
    }
}
