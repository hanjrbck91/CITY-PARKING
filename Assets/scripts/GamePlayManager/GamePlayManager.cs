using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GamePlayManager : MonoBehaviour
{
    #region Fields
    public LevelData[] levelData;


    public GameObject[] cars;
    public GameObject[] GameLevels;
    public GameObject LoadingLevels;

    //Level completion Panel
    public GameObject CompletePanel;
    public GameObject PausePanel;
    public GameObject LevelFailPanel;

    // Declare the CarIndex variable
    int CarIndex=0;
    #endregion


    #region Start/OnDestroy
    // Start is called before the first frame update
    void Start()
    {

        // Subscribe to game events
        GameEventManager.OnLevelCompleted += HandleLevelComplete;
        GameEventManager.OnLevelFailed += HandleLevelFailed; 
        GameEventManager.OnLevelPaused += HandleLevelPaused;
        GameEventManager.OnLevelResumed += HandleLevelResumed;
        GameEventManager.OnLevelRestarted += HandleLevelRestarted;
        GameEventManager.OnNextLevel += HandleNextLevel;
       
        // Deactivate all game levels first
        for (int i = 0; i < GameLevels.Length; i++)
        {
            GameLevels[i].SetActive(false);
        }

        GameObject currentLevel=  GameLevels[PlayerPrefs.GetInt("LevelNumber")];
        currentLevel.SetActive(true);
        
        // Retrieve the stored index number from PlayerPrefs and store it in CarIndex
        CarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        // You can now use the CarIndex variable in your script
        Debug.Log("CarIndex: " + CarIndex);

        for (int i =0;i < cars.Length; i++)
        {
            cars[CarIndex].SetActive(true);
        }


    }

    private void OnDestroy()
    {
        // Unsubscribe from events when the script is destroyed
        GameEventManager.OnLevelCompleted    -= HandleLevelComplete;
        GameEventManager.OnLevelFailed -= HandleLevelFailed;
        GameEventManager.OnLevelPaused -= HandleLevelPaused;
        GameEventManager.OnLevelResumed -= HandleLevelResumed;
        GameEventManager.OnLevelRestarted -= HandleLevelRestarted;
        GameEventManager.OnNextLevel -= HandleNextLevel;
    }

    #endregion


    #region Methods
    public void HandleLevelComplete()
    {
        CompletePanel.SetActive(true);

        // Get the current level number from PlayerPrefs
        int currentLevelNumber = PlayerPrefs.GetInt("LevelNumber");

        // Increment the level number
        int nextLevelNumber = currentLevelNumber + 1;

        // Check if the next level number is within the array bounds
        if (nextLevelNumber < GameLevels.Length)
        {
            // Unlock the next level using the LevelData Scriptable Object
            levelData[nextLevelNumber].isUnlocked = true;
        }

        Time.timeScale = 0;
    }


    public void HandleLevelFailed()
    {
        LevelFailPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void HandleLevelPaused()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void HandleLevelResumed()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void HandleLevelRestarted()
    {
        LoadingLevels.SetActive(true);
        Time.timeScale = 1; // Set the time scale back to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void Home()
    {
        SceneManager.LoadScene("UserInterface 1");
        Time.timeScale= 1;

    }


    public void HandleNextLevel()
    {
        LoadingLevels.SetActive(true);
        Time.timeScale = 1;
        CompletePanel.SetActive(false);

        // Get the current level number from PlayerPrefs
        int currentLevelNumber = PlayerPrefs.GetInt("LevelNumber");

        // Increment the level number
        int nextLevelNumber = currentLevelNumber + 1;

        // Check if the next level number is within the array bounds
        if (nextLevelNumber < GameLevels.Length)
        {
            // Disable the current level
            GameObject currentLevel = GameLevels[currentLevelNumber];
            currentLevel.SetActive(false);

            // Enable the next level
            GameObject nextLevel = GameLevels[nextLevelNumber];
            nextLevel.SetActive(true);

            // Set the new level number in PlayerPrefs
            PlayerPrefs.SetInt("LevelNumber", nextLevelNumber);

            // No level unlocking logic here anymore

            // Reload the scene to apply the changes
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // If the next level number exceeds the maximum index, load a game over scene or any other appropriate action
            // For example, you can go back to the main menu
            SceneManager.LoadScene("UserInterface 1");
        }
    }

    #endregion


}
