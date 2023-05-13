using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public static int numberOfLvl = 0;
    private static string[] lvls = { "SampleScene" };
    private static string[] scenes = { "MainMenu", "LvlSelectScene" };




    public static void Load(int i,bool islvlload) {
        SceneManager.LoadScene("LoadingScene");
        if (islvlload)
        {
            if (i <= numberOfLvl)
            {
                SceneManager.LoadScene(lvls[i]);
            }
            else
            {
                SceneManager.LoadScene(lvls[numberOfLvl]);
            }
        }
        else {
            SceneManager.LoadScene(scenes[i]);
        }
    }


}
