using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMain : MonoBehaviour
{

    public GameObject main;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = main.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            gameObject.SetActive(true);
            
            animator.SetTrigger("Falling");
        }

    }


}
