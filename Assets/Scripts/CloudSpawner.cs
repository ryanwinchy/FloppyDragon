using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnCloud", 0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCloud()
    {
        int index = Random.Range(0, cloudPrefabs.Length);
        Instantiate(cloudPrefabs[index], new Vector3(Random.Range(35f, 40f), Random.Range(-1f, 19f), 5.19f), cloudPrefabs[index].transform.rotation);
    }
}
