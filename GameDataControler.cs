using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameDataControl : MonoBehaviour {

    public static GameDataControl gdControl;
    public int coinsEarned;
    public int coinsSpent;
    public int coinsTotal;
    public List<int> blocksUnlocked;
    public List<List<int>> all_level_results;
    public List<List<int>> blockSecretsUnlocked;
    public bool hardUnlocked;
    public List<int> daily_level_results;
    public List<int> daily_easyLevel_Indexes;
    public List<int> daily_hardLevel_Indexes;
    public int profile_puzzlesSolved;

    private bool resetLoaded = false;

    private void Awake() {
        //Run this on first time ever opening game.
        int firstTime = PlayerPrefs.GetInt("firstTime", 0);
        if (firstTime == 0) {
            PlayerPrefs.SetInt("firstTime", 20);
            Debug.Log("First Time Opening Game!");
            FindObjectOfType<SplashScreenMechanics>().firstTime = true;
            ResetPlayerData();
            resetLoaded = true;
        }

        if (gdControl == null) {
            DontDestroyOnLoad(gameObject);
            gdControl = this;
            if (!resetLoaded) {
                LoadPlayerData();
            }
        }
        else if (gdControl != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //PlayerPrefs.DeleteAll();
        //Debug.Log(Application.persistentDataPath);
    }

    private void PrintAllLevelResults() {
        int blocksIndex = gdControl.all_level_results.Count;
        for (int i = 0; i < blocksIndex; i++) {
            string lvlResultsString = "";
            for (int x = 0; x < all_level_results[i].Count; x++) {
                lvlResultsString += all_level_results[i][x].ToString();
            }
            Debug.Log("Block " + i + " All Level Results " + lvlResultsString);
        }
    }

    public List<int> GetFullSecretsUnlocked(){
        List<int> SecretsUnlocked = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        int blocksIndex = gdControl.blockSecretsUnlocked.Count;
        for (int i = 0; i < blocksIndex; i++){
            int firstSecret = gdControl.blockSecretsUnlocked[i][0];
            int secondSecret = gdControl.blockSecretsUnlocked[i][1];
            int thirdSecret = gdControl.blockSecretsUnlocked[i][2];
            int fourthSecret = gdControl.blockSecretsUnlocked[i][3];
            if (firstSecret == 1 && secondSecret == 1 && thirdSecret == 1 && fourthSecret == 1){
                SecretsUnlocked[i] = 1;
            }
        }

        return SecretsUnlocked;
    }

    public void PrintSecretResults() {
        int blocksIndex = gdControl.blockSecretsUnlocked.Count;
        for (int i = 0; i < blocksIndex; i++) {
            int firstSecret = gdControl.blockSecretsUnlocked[i][0];
            int secondSecret = gdControl.blockSecretsUnlocked[i][1];
            int thirdSecret = gdControl.blockSecretsUnlocked[i][2];
            int fourthSecret = gdControl.blockSecretsUnlocked[i][3];
            int firstLevel = gdControl.blockSecretsUnlocked[i][4];
            int secondLevel = gdControl.blockSecretsUnlocked[i][5];
            int thirdLevel = gdControl.blockSecretsUnlocked[i][6];
            int fourthLevel = gdControl.blockSecretsUnlocked[i][7];
            Debug.Log("Block " + i + " Secret Results " + firstSecret + " " + secondSecret + " " + thirdSecret + " " + fourthSecret+" "+ firstLevel + " " + secondLevel + " " + thirdLevel + " " + fourthLevel);
        }
    }

    public void SavePlayerData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/bluePathPlayerData2.dat");

        PlayerData data = new PlayerData();

        data.coinsEarned = coinsEarned;
        data.coinsSpent = coinsSpent;
        data.coinsTotal = coinsTotal;
        data.blocksUnlocked = blocksUnlocked;
        data.all_level_results = all_level_results;
        data.blockSecretsUnlocked = blockSecretsUnlocked;
        data.hardUnlocked = hardUnlocked;
        data.daily_level_results = daily_level_results;
        data.daily_easyLevel_Indexes = daily_easyLevel_Indexes;
        data.daily_hardLevel_Indexes = daily_hardLevel_Indexes;
        data.profile_puzzlesSolved = profile_puzzlesSolved;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved Player Data To Local File");
    }

    public void LoadPlayerData() {
        if (File.Exists(Application.persistentDataPath + "/bluePathPlayerData2.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/bluePathPlayerData2.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();


            coinsEarned = data.coinsEarned;
            coinsSpent = data.coinsSpent;
            coinsTotal = data.coinsTotal;
            blocksUnlocked = data.blocksUnlocked;
            all_level_results = data.all_level_results;
            blockSecretsUnlocked = data.blockSecretsUnlocked;
            hardUnlocked = data.hardUnlocked;
            daily_level_results = data.daily_level_results;
            daily_easyLevel_Indexes = data.daily_easyLevel_Indexes;
            daily_hardLevel_Indexes = data.daily_hardLevel_Indexes;
            profile_puzzlesSolved = data.profile_puzzlesSolved;
            Debug.Log("Loaded Player Data from Local File");

        }
        else {
            Debug.Log("No Player Data found re-creating Player Data");
            ResetPlayerData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //PrintAllLevelResults();
            //PrintSecretResults();
        }
    }

    public void ResetPlayerData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/bluePathPlayerData2.dat");
        //Debug.Log(Application.persistentDataPath);
        PlayerData data = new PlayerData();

        data.coinsEarned = 0;
        data.coinsSpent = 0;
        data.coinsTotal = 0;
        data.blocksUnlocked = new List<int> { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        data.all_level_results = new List<List<int>> {
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }};
        data.blockSecretsUnlocked = new List<List<int>> {
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 }};
        data.hardUnlocked = false;
        data.daily_level_results = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        data.daily_easyLevel_Indexes = new List<int> { 0, 0, 0, 0, 0 };
        data.daily_hardLevel_Indexes = new List<int> { 0, 0, 0, 0, 0 };
        data.profile_puzzlesSolved = 0;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Reset Player Data in Local File");

        LoadPlayerData();
    }

    public void AddCoins(int ammount) {
        coinsEarned += ammount;
        coinsTotal += ammount;
    }

    public void AddPuzzleSolved() {
        profile_puzzlesSolved++;
    }

}


[Serializable]
class PlayerData {
    public int coinsEarned;
    public int coinsSpent;
    public int coinsTotal;
    public List<int> blocksUnlocked;
    public List<List<int>> all_level_results;
    public List<List<int>> blockSecretsUnlocked;
    public bool hardUnlocked;
    public List<int> daily_level_results;
    public List<int> daily_easyLevel_Indexes;
    public List<int> daily_hardLevel_Indexes;
    public int profile_puzzlesSolved;
}
