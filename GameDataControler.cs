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

    private void Awake() {
        //Check if game has been opened before
        bool firstTimeOpeningGame = PlayerPrefs.GetBool("firstTimeOpeningGame", true);
        
        if (firstTimeOpeningGame) {
            PlayerPrefs.SetBool("firstTimeOpeningGame", false);
            Debug.Log("First Time Opening Game!");
            ResetPlayerData();
        }
        
        //Singelton pattern
        if (gdControl == null) {
            DontDestroyOnLoad(gameObject);
            gdControl = this;
            LoadPlayerData();
        }
        else{
            Destroy(gameObject);
        }
    }

    public void SavePlayerData() {
        //Write GameData to a binary file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");

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
        //If GameData exists read it into the controller
        if (File.Exists(Application.persistentDataPath + "/game.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game.dat", FileMode.Open);
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
        //If GameData doesn't exist Reset Player Data and reload scene
        else {
            Debug.Log("No Player Data found re-creating Player Data");
            ResetPlayerData();
            //Reload scene after resetting player data
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetPlayerData() {
        //Write new empty GameData to a binary file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");
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
