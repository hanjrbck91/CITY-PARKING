using System;
using System.Collections;
using System.Collections.Generic;

public class GameEventManager
{
    #region  Events/Actions
    public static event Action OnLevelCompleted;
    public static event Action OnLevelFailed;
    public static event Action OnCheckPointTrigger;

    public static event Action OnLevelPaused;
    public static event Action OnLevelResumed;
    public static event Action OnLevelRestarted;
    public static event Action OnNextLevel;
    #endregion


    #region InvokeEvent
    public void LevelCompleted() => OnLevelCompleted?.Invoke();
    public void LevelFailed() => OnLevelFailed?.Invoke();
    public void LevelPaused() => OnLevelPaused?.Invoke();
    public void LevelResumed() => OnLevelResumed?.Invoke();
    public void LevelRestarted() => OnLevelRestarted?.Invoke();
    public void NextLevel() => OnNextLevel?.Invoke();
    public void CheckPointTrigger() => OnCheckPointTrigger?.Invoke();   

    #endregion
}
