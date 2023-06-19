using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static int numberOfLvl = 1;
    private static string[] _lvls = {"Level_1","SampleScene"};
    private static string[] _scenes = {"MainMenu", "LvlSelectScene"};
    private static string targetLvl = _lvls[0];

    public static void Load(int i, bool islvlload)
    {
        Time.timeScale = 1;
        GameState.isSceneEnd = true;
        SceneManager.LoadScene("LoadingScene");
        GameState.isSceneEnd = false;
        if (islvlload)
        {
            targetLvl = i <= numberOfLvl ? _lvls[i] : _lvls[0];
            SceneManager.LoadScene(targetLvl);
        }
        else
            SceneManager.LoadScene(_scenes[i]);
        
    }

    public static void Load()
    {
        GameState.isSceneEnd = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("LoadingScene");
        GameState.isSceneEnd = false;
        SceneManager.LoadScene(targetLvl);
        
    }

    public static void LoadNextLevel()
    {
        GameState.isSceneEnd = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("LoadingScene");
        GameState.isSceneEnd = false;
        int i = Array.IndexOf(_lvls, targetLvl) + 1;
        targetLvl = i + 1 <= numberOfLvl ? _lvls[i + 1] : "MainMenu";
        SceneManager.LoadScene(targetLvl);
        
    }

}