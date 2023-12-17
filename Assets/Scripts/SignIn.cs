using TMPro;
using UnityEngine;

public class SignIn : MonoBehaviour
{
    [SerializeField] private SaveLoadManager loadManager;
    [SerializeField] private GameObject currentCanvas;
    [SerializeField] private GameObject nextCanvas;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_Text errorText;

    public string LoggedEmail { get; set; }
    private UserData userData;

    public void Validate()
    {
        if (!CheckField(out string errorMessage) || !CheckLogin(out errorMessage))
        {
            errorText.text = errorMessage;
            errorText.gameObject.SetActive(true);
        }
        else
        {
            LoggedEmail = userData.email;
            errorText.gameObject.SetActive(false);
            currentCanvas.SetActive(false);
            nextCanvas.SetActive(true);
        }
    }

    private bool CheckField(out string errorMessage)
    {
        errorMessage = string.Empty;
        if (string.IsNullOrEmpty(email.text))
        {
            errorMessage = "Informe o Email";
            return false;
        }
        else if (string.IsNullOrEmpty(password.text))
        {
            errorMessage = "Informe a Senha";
            return false;
        }

        return true;
    }

    private bool CheckLogin(out string errorMessage)
    {
        errorMessage = string.Empty;
        userData = loadManager.LoadUser(email.text);
        if (userData != null)
        {
            if(userData.password == password.text)
            {
                return true;
            }
            else
            {
                errorMessage = "Senha Incorreta";
                return false;
            }
        }
        else
        {
            errorMessage = "Email não cadastrado";
            return false;
        }
    }
}
