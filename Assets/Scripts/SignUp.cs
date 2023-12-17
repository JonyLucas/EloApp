using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class SignUp : MonoBehaviour
{
    [SerializeField] private SaveLoadManager saveManager;
    [SerializeField] private SignIn signIn;
    [SerializeField] GameObject currentCanvas;
    [SerializeField] GameObject nextCanvas;
    [SerializeField] private TMP_InputField email;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField confirmPassword;
    [SerializeField] private TMP_InputField city;
    [SerializeField] private TMP_InputField state;
    [SerializeField] private TMP_Text errorText;
    private int profession = 0;
    private bool useTerms;

    public void SetProfession(int profession)
    {
        this.profession = profession;
    }

    public void SetUseTerms(bool useTerms)
    {
        this.useTerms = useTerms;
    }

    public void ValideInputs()
    {
        var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var regex = new Regex(pattern);

        if (!regex.IsMatch(email.text))
        {
            errorText.text = "Email Inv�lido";
            errorText.gameObject.SetActive(true);
            return;
        }

        if(string.IsNullOrEmpty(password.text) ||
            string.IsNullOrEmpty(confirmPassword.text) || 
            string.IsNullOrEmpty(city.text) ||
            string.IsNullOrEmpty(state.text) || !useTerms) 
        {
            errorText.text = "*Preenchimento Obrigat�rio";
            errorText.gameObject.SetActive(true);
            return;
        }

        if(string.Compare(password.text, confirmPassword.text) != 0)
        {
            errorText.text = "As senhas n�o correspondem";
            errorText.gameObject.SetActive(true);
            return;
        }

        SaveData();
        signIn.LoggedEmail = email.text;
        currentCanvas.SetActive(false);
        nextCanvas.SetActive(true);
    }

    private void SaveData()
    {
        var userData = new UserData
        {
            email = email.text,
            password = password.text,
            profession = profession,
            city = city.text,
            state = state.text,
            questions = new bool[20],
            filledForm = false,
            score = 0
        };

        saveManager.SaveData(userData);
    }
}
