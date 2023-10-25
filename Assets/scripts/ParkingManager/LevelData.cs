using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObject/LevelData")]
public class LevelData : ScriptableObject
{
    public bool isUnlocked;
    // Add more level-related data here if needed

    // Save the level data using PlayerPrefs
    public void Save()
    {
        PlayerPrefs.SetInt("Level_" + name + "_Unlocked", isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }
}
