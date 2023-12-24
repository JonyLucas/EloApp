using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldSelection : MonoBehaviour
{
    private TMP_InputField inputField;
    
    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
    }
    
    public void OnFocus()
    {
        KeyboardManager.Instance.ShowKeyboard(inputField);
    }
}
