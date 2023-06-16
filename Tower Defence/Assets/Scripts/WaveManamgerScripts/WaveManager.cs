using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [System.Serializable] public class SubList{
        public string WaveName;
        [Range(0,60)]public float waitTimeAfterWave;
        [Range(0,60)]public float timeBetween;
        public Transform spawnPoint;
        public List<wavePart> Wave = new List<wavePart>();
    }

    [System.Serializable] public class wavePart{
        [Range(0,60)]public float timeBetweenEnemy;
        [Range(0,100)]public int count;
        public GameObject enemy;
        public Transform spawnPoint;
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
            
            yield return StartCoroutine(WaveSpawner(waves[i].Wave,waves[i].timeBetween,waves[i].spawnPoint.position));
            
            yield return StartCoroutine(WaitEndWave());

            
        }
       
    }
    private IEnumerator WaveSpawner(List<wavePart> wave,float timeBetween,Vector3 spawnPoint){
        for(int i = 0; i<wave.Count;i++){
            yield return new WaitForSeconds(timeBetween);
            for(int k =0;k<wave[i].count;k++){
                if(wave[i].spawnPoint == null){
                    EnemySpawner(wave[i].enemy,spawnPoint);
                }else{
                    EnemySpawner(wave[i].enemy,wave[i].spawnPoint.position);
                }
                yield return new WaitForSeconds(wave[i].timeBetweenEnemy);
            }
        }
    }
    private void EnemySpawner(GameObject enemy,Vector3 spawnPoint){
        Instantiate(enemy,spawnPoint,Quaternion.identity);
    }
    private IEnumerator WaitEndWave(){
        yield return new WaitUntil(() => GameObject.FindWithTag("Enemy") == null);
    }
}


