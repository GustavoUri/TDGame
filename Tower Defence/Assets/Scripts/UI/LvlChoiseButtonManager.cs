using UnityEngine;
using UnityEngine.UI;

public class LvlChoiseButtonManager : MonoBehaviour
{

    public Button lvl1Button;
    public Button backButton;


    private void Awake() {
        lvl1Button.onClick.AddListener(() => {
            Loader.Load(0,true);
        });

        backButton.onClick.AddListener(() => {
            Loader.Load(0, false);
        });


    }
}
