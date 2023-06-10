using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class SoundPlayer: MonoBehaviour
{
    public AudioClip[] backgroungMusic;
    public AudioClip[] shotSound;
    public AudioClip[] hitEnemySound;
    public AudioClip[] enemyDeathSound;
    public AudioClip[] stealingSound;
    public AudioClip[] buttonSound; //???? возможно нафиг не нужен
    public AudioClip[] lvlProgretionSound; //???? возможно нафиг не нужен
    public AudioClip[] towerCreateSound;

    public static SoundPlayer singltonSoundPlayer { get; private set; }

    private void Start() {
        singltonSoundPlayer = this;

    }


    public static void PlayShoutSound(GameObject volumeSourse) {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.shotSound[0], new Vector3(5, 1, 2));
    }

    public static void PlayShoutSound(Vector3 volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.shotSound[0], new Vector3(5, 1, 2));
    }

    public static void PlayHitSound(GameObject volumeSourse)
    {
        AudioSource.PlayClipAtPoint(singltonSoundPlayer.hitEnemySound[0], volumeSourse.transform.position);
    }

}
