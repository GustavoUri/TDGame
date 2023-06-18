using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonManager : MonoBehaviour
{

    public Button startButton;
    public Button lvlButton;
    public Button soundButton;
    //public Button exitButton;


    private void Awake() {
        startButton.onClick.AddListener(() => {
            Loader.Load(PlayerProgression.lastCompliteLvl,true);
        });

        lvlButton.onClick.AddListener(() => {
            Loader.Load(1, false);
        });


        soundButton.onClick.AddListener(() => {
            SoundPlayer.ReverseSound();
        });

        //exitButton.onClick.AddListener(() => {
        //Application.Quit();
        //});

    }
}
