using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject pepperPrefab;

    private PlayerController playerController;

    public AudioClip iSeeFire;

    private GlobalSettings global;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 3f, 1f);
        InvokeRepeating("AttemptSpawnPepper", 3f, 1f);

        playerController = GameObject.Find("Dragon").GetComponent<PlayerController>();
        global = GameObject.Find("SceneManager").GetComponent<GlobalSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, new Vector3(30f, Random.Range(3.89f, 12.78f), 0), obstaclePrefab.transform.rotation);
    }

    void AttemptSpawnPepper()
    {
        if (!playerController.hasPepper)
        {
            int spawn = Random.Range(0, 45);    //1 in 11 chance to spawn!
            if (spawn == 0)
            {
                Instantiate(pepperPrefab, pepperPrefab.transform.position, obstaclePrefab.transform.rotation);
                playerController.audioSource.volume = global.GetVolume();
                playerController.audioSource.PlayOneShot(iSeeFire);

            }

        }
  



    }

}
