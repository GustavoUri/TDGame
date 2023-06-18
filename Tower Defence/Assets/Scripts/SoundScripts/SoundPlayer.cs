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

    public static void ReverseSound() {
        if (GameState.isSoundAccitve)
        {
            GameState.isSoundAccitve = false;
            singltonSoundPlayer.backgroundPlayer.Pause();
        }
        else {
            GameState.isSoundAccitve = true;
            singltonSoundPlayer.backgroundPlayer.Play();
        }
    }


    private void Awake() {
        singltonSoundPlayer = this;
        backgroundPlayer = GetComponent<AudioSource>();
        if (GameState.isSoundAccitve) { singltonSoundPlayer.backgroundPlayer.Play(); }
        else
        { singltonSoundPlayer.backgroundPlayer.Pause(); }
    }


    public static void PlayShoutSound(GameObject volumeSourse) {
        if (GameState.isSoundAccitve && !GameState.isSceneEnd) AudioSource.PlayClipAtPoint(singltonSoundPlayer.shotSound[0], volumeSourse.transform.position);
    }


    public static void PlayHitSound(GameObject volumeSourse)
    {
        if (GameState.isSoundAccitve && !GameState.isSceneEnd) AudioSource.PlayClipAtPoint(singltonSoundPlayer.hitEnemySound[0], volumeSourse.transform.position);
    }


    public static void PlayDeathSound(GameObject volumeSourse)
    {
        if (GameState.isSoundAccitve && !GameState.isSceneEnd) AudioSource.PlayClipAtPoint(singltonSoundPlayer.enemyDeathSound[0], volumeSourse.transform.position);
    }

    public static void PlaySpawnSound(GameObject volumeSourse)
    {
        if (GameState.isSoundAccitve && !GameState.isSceneEnd) AudioSource.PlayClipAtPoint(singltonSoundPlayer.enemySpawnSound[0], volumeSourse.transform.position);
    }

    public static void PlayBuildSound(GameObject volumeSourse)
    {
        if (GameState.isSoundAccitve && !GameState.isSceneEnd) AudioSource.PlayClipAtPoint(singltonSoundPlayer.towerCreateSound[0], volumeSourse.transform.position);
    }

    public static void PlaySellSound(GameObject volumeSourse)
    {
        if (GameState.isSoundAccitve && !GameState.isSceneEnd) AudioSource.PlayClipAtPoint(singltonSoundPlayer.turretSellSound[0], volumeSourse.transform.position);
    }


}
