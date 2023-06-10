using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAudioScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        SoundPlayer.PlayBuildSound(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }


    void OnDestroy()
    {
        SoundPlayer.PlaySellSound(gameObject);
    }
}
