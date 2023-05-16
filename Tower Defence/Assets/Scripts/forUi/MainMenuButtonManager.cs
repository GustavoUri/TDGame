using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{

    public Button startButton;
    public Button lvlButton;
    public Button exitButton;


    private void Awake() {
        startButton.onClick.AddListener(() => {
            Loader.Load(PlayerProgression.lastCompliteLvl,true);
        });

        lvlButton.onClick.AddListener(() => {
            Loader.Load(1, false);
        });


        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });

    }
}
