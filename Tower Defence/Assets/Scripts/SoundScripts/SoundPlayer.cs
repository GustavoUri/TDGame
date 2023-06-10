using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SoundPlayer: MonoBehaviour
{



    public int Volume;

    public AudioClip[] enemySpawnSound;
    public AudioClip[] turretSellSound;
    public AudioClip[] shotSound;
    public AudioClip[] hitEnemySound;
    public AudioClip[] enemyDeathSound;
    public AudioClip[] stealingSound;
    public AudioClip[] lvlProgretionSound; //???? возможно нафиг не нужен
    public AudioClip[] towerCreateSound;

    private static SoundPlayer singltonSoundPlayer { get; set; }

    private AudioSource backgroundPlayer;




    private void Start() {
        singltonSoundPlayer = this;
        backgroundPlayer = GetComponent<AudioSource>();
        backgroundPlayer.Play();
    }


    public static void PlayShoutSound(GameObject volumeSourse) {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.shotSound[0], volumeSourse.transform.position);
    }


    public static void PlayHitSound(GameObject volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.hitEnemySound[0], volumeSourse.transform.position);
    }


    public static void PlayDeathSound(GameObject volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.enemyDeathSound[0], volumeSourse.transform.position);
    }

    public static void PlaySpawnSound(GameObject volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.enemySpawnSound[0], volumeSourse.transform.position);
    }

    public static void PlayBuildSound(GameObject volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.towerCreateSound[0], volumeSourse.transform.position);
    }

    public static void PlaySellSound(GameObject volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.turretSellSound[0], volumeSourse.transform.position);
    }


}
