using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeCall : MonoBehaviour
{

    [SerializeField] private GameObject menuWindow;
    public Button continueButton;
    public Button menuButton;


    private double drebezg = 0.0;

    // Start is called before the first frame update
    void Start()
    {
        menuWindow.SetActive(false);
    }

    private void Awake()
    {
        continueButton.onClick.AddListener(() => {
            menuWindow.SetActive(false);
            GameState.PauseGame(false);
        });

        menuButton.onClick.AddListener(() => {
            Loader.Load(0, false);
        });

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && (drebezg <= 0)) {
            drebezg = 0.2;
            if (menuWindow.activeSelf)
            {
                menuWindow.SetActive(false);
                GameState.PauseGame(false);
            }
            else {
                menuWindow.SetActive(true);
                GameState.PauseGame(true);
            }
        }

        if (drebezg > 0) {
            drebezg -= Time.unscaledDeltaTime;
        }

    }
}
