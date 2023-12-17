using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string email;
    public string password;
    public int profession;
    public string city;
    public string state;
    public bool[] questions;
    public bool filledForm;
    public int score;

    public static UserData CreateDefaultUser()
    {
        return new UserData
        {
            email = "tester@elo.com",
            password = "102030",
            profession = -1,
            city = string.Empty,
            state = string.Empty,
            questions = new bool[10],
            filledForm = false,
            score = 0
        };
    }
}

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        WebGLInput.mobileKeyboardSupport = true;
        // WebGLInput.captureAllKeyboardInput = true;
        filePath = Path.Combine(Application.persistentDataPath, "eloData.json");
        var loadedData = LoadData();
        if (loadedData == null || loadedData.Count == 0)
        {
            var defaultUser = UserData.CreateDefaultUser();
            SaveData(defaultUser);
        }
    }

    public void SaveData(UserData data)
    {
        var loadedData = LoadData();
        if (loadedData != null)
        {
            var userData = loadedData.FirstOrDefault(user => user.email == data.email);
            if(userData != null)
            {
                loadedData.Remove(userData);
            }
            loadedData.Add(data);
            SaveData(loadedData);
        }
        else
        {
            var newData = new List<UserData> { data };
            SaveData(newData);
        }
    }

    private void SaveData(List<UserData> data)
    {
        // Serialize the data to a JSON string
        var jsonData = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);

        // Write the JSON string to the file
        File.WriteAllText(filePath, jsonData);
    }

    private List<UserData> LoadData()
    {
        if (File.Exists(filePath))
        {
            // Read the JSON string from the file
            string jsonData = File.ReadAllText(filePath);

            // Deserialize the JSON string to a GameData object
            var data = JsonConvert.DeserializeObject<UserData[]>(jsonData);
            return data.ToList();
        }
        else
        {
            //Debug.LogError("File not found: " + filePath);
            return null;
        }
    }

    public UserData LoadUser(string email)
    {
        var loadData = LoadData();
        return loadData?.FirstOrDefault(user => user.email == email);
    }
}
