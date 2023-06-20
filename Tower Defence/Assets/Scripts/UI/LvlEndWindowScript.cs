using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LvlEndWindowScript : MonoBehaviour
{

    public Button continueButton;
    public Button restartButton;
    public Button menuButton;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        if (continueButton != null) continueButton.onClick.AddListener(() => { Loader.LoadNextLevel(); });

        if (menuButton != null) menuButton.onClick.AddListener(() => { Loader.Load(0, false); });
        if (restartButton != null) restartButton.onClick.AddListener(() => { Loader.Load(5, true); });
    }


    // Update is called once per frame
    void Update()
    {
    }
}
