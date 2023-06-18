using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlEndManager : MonoBehaviour
{

    private GameObject mainTower;
    private MainTower mainTowerScript;

    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;

    // Start is called before the first frame update
    void Awake()
    {
        loseWindow.SetActive(false);
        mainTower =  GameObject.Find("Main Tower");
        if (mainTower != null) {
            mainTowerScript = mainTower.GetComponent<MainTower>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.isSceneEnd) return;
        if (mainTower == null || mainTowerScript.Health <= 0) {
            GameState.isSceneEnd = true;
            GameState.PauseGame(true);
            loseWindow.SetActive(true);
        }

    }
}
