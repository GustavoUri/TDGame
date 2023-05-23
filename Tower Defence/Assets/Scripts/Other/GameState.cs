using UnityEngine;

public static class GameState
{
    public static bool IsGamePaused => Time.timeScale == 0;

    public static void PauseGame(bool newState)
    {
        Time.timeScale = newState ? 0 : 1;
    }
}