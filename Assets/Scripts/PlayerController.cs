using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;

    public float gravityModifier = 2;
    public float jumpForce = 200f;
    public float maxJumpVelocity = 25f;
    private bool jumping = false;

    private float lastYPos = 0;

    public bool isGameOver = false;

    public GameObject fireFX;


    Animator animator;

    public ParticleSystem wind;

    public bool hasPepper = false;

    public AudioClip slap;
    public AudioSource audioSource;

    private int score = 0;

    public TextMeshProUGUI scoreText;
    public GameObject pressFText;

    private GlobalSettings global;




    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        global = GameObject.Find("SceneManager").GetComponent<GlobalSettings>();



        playerRb.isKinematic = false;       //Stop the dragon's normal physics and gravity to play animation of dragon dropping.
        playerRb.useGravity = true;
        Physics.gravity *= gravityModifier;

       audioSource = GetComponent<AudioSource>();


        scoreText.text = score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
  

        flightDirection(lastYPos);

        if (transform.position.y < 14.5f)
            performJump();

        if ((transform.position.y < 3f) && !isGameOver)
        {
            playerRb.isKinematic = true; // Temporarily stop physics influence
            Vector3 newPosition = transform.position;
            newPosition.y = 3f;   //Current position but y as 3, the min height dragon should be (otherwise would get overriden by lower than 3 value.
            transform.position = newPosition;
            playerRb.isKinematic = false; // Restore physics
        }

        if (hasPepper && Input.GetKeyDown(KeyCode.F))
        {
            hasPepper = false;
            pressFText.SetActive(false);
            StartCoroutine(PowerupTimer());
        }


        lastYPos = transform.position.y;     //get position at end of update loop to compare to next loop.
    }

    IEnumerator PowerupTimer()
    {
        fireFX.SetActive(true);
        yield return new WaitForSeconds(7f);
        fireFX.SetActive(false);
        
    }

    private void flightDirection(float lastYpos)
    {
        if (!isGameOver)
        {
            if (transform.position.y > lastYPos)    //Travelling up
                animator.SetFloat("flapSpeed", 2.0f); // Flap twice as quickly
            if (transform.position.y < lastYPos)    //Falling
                animator.SetFloat("flapSpeed", 0.5f); // Flap at default speed when falling.
        }

    }

    private void performJump()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                jumping = true;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // Reset vertical velocity after jump.
                wind.Play();
            }

            if (jumping && playerRb.velocity.y <= 0.1f)
            {
                jumping = false;
                //animator.SetFloat("flapSpeed", 20.0f); // Reset the "JumpSpeed" parameter when the jump ends
            }

            // Limit the maximum upward velocity to ensure consistent jumps
            if (playerRb.velocity.y > maxJumpVelocity)
            {
                playerRb.velocity = new Vector3(playerRb.velocity.x, maxJumpVelocity, playerRb.velocity.z);
            }
        }


    }


    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        RestartGame();
        SceneManager.LoadScene(0);

    }

    public void RestartGame()
    {
        // Reload the browser page
        Application.ExternalEval("window.location.reload()");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioSource.volume = global.GetVolume();
            audioSource.PlayOneShot(slap);
            playerRb.isKinematic = true;       //Stop the dragon's normal physics and gravity to play animation of dragon dropping.
            playerRb.useGravity = false;

            animator.SetFloat("flapSpeed", 0.1f);
            animator.SetTrigger("Falling");
            isGameOver = true;
            fireFX.SetActive(false);
            StartCoroutine(EndGame());
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pepper"))
        {
            Destroy(other.gameObject);
            hasPepper = true;
            pressFText.SetActive(true);
        }

        if (other.CompareTag("Point"))
        {
            score++;
            scoreText.text = "" + score;
        }



    }



}
