using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameDataController : MonoBehaviour {

    public static GameDataController gdControl;

    public int coinsEarned;
    public int coinsSpent;
    public int coinsTotal;
    public List<int> campaign_blocksUnlocked;
    public List<List<int>> campaign_levelResults;
    public List<List<int>> campaign_secretsUnlocked;
    public bool dailyPuzzle_hardUnlocked;
    public List<int> dailyPuzzle_levelResults;
    public List<int> dailyPuzzle_easyLevels;
    public List<int> dailyPuzzle_hardLevels;
    public int profile_puzzlesSolved;

    private void Awake() {
        //Check if game has been opened before
        bool firstTimeOpeningGame = PlayerPrefs.GetBool("firstTimeOpeningGame", true);

        if (firstTimeOpeningGame) {
            PlayerPrefs.SetBool("firstTimeOpeningGame", false);
            Debug.Log("First Time Opening Game!");
            //Write a new empty file
            ResetGameData();
        }

        //Singelton pattern
        if (gdControl == null) {
            DontDestroyOnLoad(gameObject);
            gdControl = this;
            LoadGameData();
        }
        else {
            Destroy(gameObject);
        }
    }

    public void SaveGameData() {
        //Write GameData to a binary file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");

        GameData data = new GameData();

        data.coinsEarned = coinsEarned;
        data.coinsSpent = coinsSpent;
        data.coinsTotal = coinsTotal;
        data.campaign_blocksUnlocked = campaign_blocksUnlocked;
        data.campaign_levelResults = campaign_levelResults;
        data.campaign_secretsUnlocked = campaign_secretsUnlocked;
        data.dailyPuzzle_hardUnlocked = dailyPuzzle_hardUnlocked;
        data.dailyPuzzle_levelResults = dailyPuzzle_levelResults;
        data.dailyPuzzle_easyLevels = dailyPuzzle_easyLevels;
        data.dailyPuzzle_hardLevels = dailyPuzzle_hardLevels;
        data.profile_puzzlesSolved = profile_puzzlesSolved;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved GameData To Local File");
    }

    public void LoadGameData() {
        //If GameData exists read it into the controller
        if (File.Exists(Application.persistentDataPath + "/game.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/game.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            coinsEarned = data.coinsEarned;
            coinsSpent = data.coinsSpent;
            coinsTotal = data.coinsTotal;
            campaign_blocksUnlocked = data.campaign_blocksUnlocked;
            campaign_levelResults = data.campaign_levelResults;
            campaign_secretsUnlocked = data.campaign_secretsUnlocked;
            dailyPuzzle_hardUnlocked = data.dailyPuzzle_hardUnlocked;
            dailyPuzzle_levelResults = data.dailyPuzzle_levelResults;
            dailyPuzzle_easyLevels = data.dailyPuzzle_easyLevels;
            dailyPuzzle_hardLevels = data.dailyPuzzle_hardLevels;
            profile_puzzlesSolved = data.profile_puzzlesSolved;

            Debug.Log("Loaded GameData from Local File");
        }
        //If GameData doesn't exist Reset GameData and reload scene
        else {
            Debug.Log("No GameData found re-creating GameData");
            ResetGameData();
            //Reload scene after resetting GameData
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetGameData() {
        //Write new empty GameData to a binary file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game.dat");
        GameData data = new GameData();

        data.coinsEarned = 0;
        data.coinsSpent = 0;
        data.coinsTotal = 0;
        data.campaign_blocksUnlocked = new List<int> { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        data.campaign_levelResults = new List<List<int>> {
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
        data.campaign_secretsUnlocked = new List<List<int>> {
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
        data.dailyPuzzle_hardUnlocked = false;
        data.dailyPuzzle_levelResults = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        data.daily_easyLevel_Indexes = new List<int> { 0, 0, 0, 0, 0 };
        data.daily_hardLevel_Indexes = new List<int> { 0, 0, 0, 0, 0 };
        data.profile_puzzlesSolved = 0;

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Reset GameData to Local File");
        LoadGameData();
    }

}

//Class for the GameData to hold all the variables
[Serializable]
class GameData {
    public int coinsEarned;
    public int coinsSpent;
    public int coinsTotal;
    public List<int> campaign_blocksUnlocked;
    public List<List<int>> campaign_levelResults;
    public List<List<int>> campaign_secretsUnlocked;
    public bool dailyPuzzle_hardUnlocked;
    public List<int> dailyPuzzle_levelResults;
    public List<int> dailyPuzzle_easyLevels;
    public List<int> dailyPuzzle_hardLevels;
    public int profile_puzzlesSolved;
}

