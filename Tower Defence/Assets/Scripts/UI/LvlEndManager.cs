using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlEndManager : MonoBehaviour
{
    private int suspendCheck = 0;
    private GameObject mainTower;
    private MainTower mainTowerScript;

    private GameObject waveMananager;
    private WaveManager wave;
    public bool enemyIsGone;

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

        winWindow.SetActive(false);
        waveMananager = GameObject.Find("WaveManager");
        if (waveMananager != null)
        {
            wave = waveMananager.GetComponent<WaveManager>();
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.isSceneEnd) return;
        if (mainTower == null || (mainTowerScript.Health <= 0 && enemyIsGone)) {
            GameState.isSceneEnd = true;
            GameState.PauseGame(true);
            loseWindow.SetActive(true);
        }
        if (waveMananager == null || wave.isSpawnEnd)
        {
            if (suspendCheck == 60) {
                suspendCheck = 0;
                if (GameObject.FindWithTag("Enemy") == null) {
                    GameState.isSceneEnd = true;
                    GameState.PauseGame(true);
                    winWindow.SetActive(true);
                }
            }
            suspendCheck++;
        }
    }
}
