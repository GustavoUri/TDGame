using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerAudioScript : MonoBehaviour
{
    // Start is called before the first frame update

    private int lastHealth;
    private MainTower mainTowerScript;


    void Start()
    {
        mainTowerScript = gameObject.GetComponent<MainTower>();
        lastHealth = mainTowerScript.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastHealth < mainTowerScript.Health) {
            Debug.Log(lastHealth);
            lastHealth = mainTowerScript.Health;
            SoundPlayer.PlayStealSound(gameObject);
        }
    }
}
