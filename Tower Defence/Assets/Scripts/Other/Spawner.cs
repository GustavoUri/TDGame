using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public float countdown = 5;

    public float timeBetween = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            countdown = timeBetween;
        }

        countdown -= Time.deltaTime;
    }
}
