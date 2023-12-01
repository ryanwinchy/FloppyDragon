using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    private float speed = 15.0f;
    private GameObject dragonPrefab;
    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        dragonPrefab = GameObject.FindGameObjectWithTag("Player");
        playerControllerScript = dragonPrefab.GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isGameOver == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            if (transform.position.x < -15)   //Deswpawn.
                Destroy(gameObject);
        }


    }

    private void OnParticleCollision(GameObject other)
    {
        
        

            Destroy(gameObject);
        

    }

}
