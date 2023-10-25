using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variable/Data
    [Header("MainMenu")]
    [SerializeField]
    public GameObject menuPanel;
    public GameObject exitPanel;
    public GameObject Loading;

    [Space(5)]
    [Header("LevelSelection")]
    public GameObject levelSelection;

    public GameObject[] LevelButtons; // Array of level buttons
    public GameObject[] LockButtons; // Array of lock buttons
    public LevelData[] levelData; // Add this array to hold the LevelData instances

    [Space(5)]
    [Header("CarSelection")]
    public GameObject carSelection;
    public GameObject[] CarsList;

    public GameObject selected;
    public GameObject select;


    [Space(5)]
    [Header("General")]
    public GameObject canvas;
    #endregion

    #region Start/Update
    // Start is called before the first frame update
    void Start()
    {

        // Loop through the levels and check their unlock status using LevelData instances
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            // By default, lock all buttons
            LevelButtons[i].SetActive(false);
            LockButtons[i].SetActive(true);

            // Check if the level is unlocked
            if (levelData[i].isUnlocked)
            {
                // Level is unlocked, activate the button and deactivate the lock
                LevelButtons[i].SetActive(true);
                LockButtons[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region MainMenu
    public void Exit()
    {
        exitPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void Yes()
    {
        // Reset the level data to default state (lock all levels except level 1)
        for (int i = 1; i < levelData.Length; i++)
        {
            levelData[i].isUnlocked = false;
            levelData[i].Save(); // Save the changes for each level
        }

        // Unlock level 1
        levelData[0].isUnlocked = true;
        levelData[0].Save(); // Save the change for level 1

        // Quit the application
        Application.Quit();
    }

    public void No()
    {
        exitPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void Rate()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=8dTXkvIswl0&list=PLeDTi8nLAyNmqvko0Jh1Kf4TJD4Zc_Omn&ab_channel=SparkCode");
    }

    public void Store()
    {
        carSelection.SetActive(true);
        menuPanel.SetActive(false);
        //canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
    }
    // function for GO! and back  button in carselection in order to disable canvas camera
    public void DisableCanvasCamera()
    {
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    }

    public void Play()
    {

        levelSelection.SetActive(true);
        menuPanel.SetActive(false);

    }
    #endregion

    #region LevelSelection
    public void BackFromLevelSelection()
    {
        menuPanel.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void LevelSelection(int levelNo)
    {
        PlayerPrefs.SetInt("LevelNumber", levelNo);

        // Save the selected car index in PlayerPrefs
        //PlayerPrefs.SetInt("SelectedCar", carListCounter);

        Loading.SetActive(true);
        SceneManager.LoadScene("level-1");
        Time.timeScale = 1.0f;
    }

    #endregion

    #region CarSelection

    int carListCounter = 0;

    public void NextCar()
    {
        carListCounter = (carListCounter + 1) % CarsList.Length;
        ShowCar(carListCounter);
    }

    public void PrevCar()
    {
        carListCounter = (carListCounter - 1 + CarsList.Length) % CarsList.Length;
        ShowCar(carListCounter);
    }

    public void SelectButton()
    {
        selected.SetActive(true);
        select.SetActive(false);

        PlayerPrefs.SetInt("SelectedCar", carListCounter);
        Debug.Log("Selected Car Index: " + PlayerPrefs.GetInt("SelectedCar"));


    }

    public void UnSelectButton()
    {
        select.SetActive(true);
        selected.SetActive(false);
    }

    // Helper function to show the selected car
    private void ShowCar(int index)
    {
        foreach (var item in CarsList)
        {
            item.SetActive(false);
        }

        CarsList[index].SetActive(true);

        // Check if this car is the selected one and show/hide the buttons accordingly
        if (PlayerPrefs.GetInt("SelectedCar", 0) == index)
        {
            selected.SetActive(true);
            select.SetActive(false);
        }
        else
        {
            selected.SetActive(false);
            select.SetActive(true);
        }
    }


    #endregion


}



