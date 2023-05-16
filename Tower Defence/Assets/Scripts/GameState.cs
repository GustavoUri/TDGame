using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{

    public static bool isGamePaused() {
        return Time.timeScale == 0;
    }

    public static void PauseGame(bool newState) {
        if (newState)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }
    }
    

}
