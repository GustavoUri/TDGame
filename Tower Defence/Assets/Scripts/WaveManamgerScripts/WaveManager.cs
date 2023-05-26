using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable] public class SubList{
        public string WaveName;
        public float waitTimeAfterWave;
        public float timeBetween;
        public Transform spawnPoint;
        public List<GameObject> Wave = new List<GameObject>();
    }
    [SerializeField] private List<SubList> Waves;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WavesSpawner(Waves));
    }

    private IEnumerator WavesSpawner(List<SubList> waves){
        for(int i = 0; i <waves.Count;i++){
            yield return new WaitForSeconds(waves[i].waitTimeAfterWave);
            StartCoroutine(WaveSpawner(waves[i].Wave,waves[i].timeBetween,waves[i].spawnPoint.position));
        }
       
    }
    private IEnumerator WaveSpawner(List<GameObject> wave,float timeBetween,Vector3 spawnPoint){
        for(int i = 0; i<wave.Count;i++){
            yield return new WaitForSeconds(timeBetween);
            EnemySpawner(wave[i],spawnPoint);
        }
    }
    private void EnemySpawner(GameObject enemy,Vector3 spawnPoint){
        Instantiate(enemy,spawnPoint,Quaternion.identity);
    }


}
