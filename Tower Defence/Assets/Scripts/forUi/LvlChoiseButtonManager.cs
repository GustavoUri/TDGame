using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlChoiseButtonManager : MonoBehaviour
{

    public Button lvl1Button;
    public Button BackButton;


    private void Awake() {
        lvl1Button.onClick.AddListener(() => {
            Loader.Load(0,true);
        });

        BackButton.onClick.AddListener(() => {
            Loader.Load(0, false);
        });


    }
}
