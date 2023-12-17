using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField]
    private SignIn signIn;

    [SerializeField]
    private SaveLoadManager saveLoadManager;

    [SerializeField]
    private bool[] teaCondition;

    [SerializeField]
    private GameObject scoreTable;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private GameObject descriptionTable;

    [SerializeField]
    private FormLine[] formLines;

    private int score;


    private void OnEnable()
    {
        score = 0;

        if (string.IsNullOrEmpty(signIn.LoggedEmail))
        {
            return;
        }

        var userData = saveLoadManager.LoadUser(signIn.LoggedEmail);
        if (userData.filledForm)
        {
            for (var i = 0; i < formLines.Length; i++)
            {
                if (userData.questions[i])
                {
                    formLines[i].YesToggle.isOn = true;
                }
                else
                {
                    formLines[i].NoToggle.isOn = true;
                }
            }

            CalculateScore();
        }
    }

    public void CalculateScore()
    {
        if (string.IsNullOrEmpty(signIn.LoggedEmail))
        {
            return;
        }

        score = 0;
        var awnsredQuestions = new bool[formLines.Length];

        for (var i = 0; i < formLines.Length; i++)
        {
            awnsredQuestions[i] = formLines[i].YesToggle.isOn;
            if (teaCondition[i] == formLines[i].YesToggle.isOn)
            {
                score++;
            }
        }

        scoreTable.SetActive(true);
        descriptionTable.SetActive(true);
        scoreText.text = score.ToString();

        var userData = saveLoadManager.LoadUser(signIn.LoggedEmail);
        userData.filledForm = true;
        userData.questions = awnsredQuestions;
        saveLoadManager.SaveData(userData);
    }

}
