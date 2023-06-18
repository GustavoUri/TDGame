using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static int numberOfLvl = 0;
    private static string[] _lvls = {"SampleScene"};
    private static string[] _scenes = {"MainMenu", "LvlSelectScene"};


    public static void Load(int i, bool islvlload)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LoadingScene");
        if (islvlload)
            SceneManager.LoadScene(i <= numberOfLvl ? _lvls[i] : _lvls[numberOfLvl]);
        else
            SceneManager.LoadScene(_scenes[i]);
    }
}