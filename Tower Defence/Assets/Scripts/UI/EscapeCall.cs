using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeCall : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;
    public Button continueButton;
    public Button menuButton;


    // Start is called before the first frame update
    void Start()
    {
        menuWindow.SetActive(false);
    }

    private void Awake()
    {
        continueButton.onClick.AddListener(() =>
        {
            menuWindow.SetActive(false);
            GameState.PauseGame(false);
        });

        menuButton.onClick.AddListener(() => { Loader.Load(0, false); });
    }


    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyUp(KeyCode.Escape)) return;
        if (menuWindow.activeSelf)
        {
            menuWindow.SetActive(false);
            GameState.PauseGame(false);
        }
        else
        {
            menuWindow.SetActive(true);
            GameState.PauseGame(true);
        }
    }
}