using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public Text scoreText;
    public Text healthText;
    int health;
    int score;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip itemSound;
    public AudioClip jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
        healthText.text = "Health: " + health + "%";
        scoreText.text = "Score: 0";

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            rb.velocity = new Vector2(0, jump);
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(jumpSound);

        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // else if (Input.GetAxis("Horizontal") < 0)
        // {
        //     transform.Translate(-speed, 0, 0);
        //     sr.flipX = true;
        // }

    }
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            // transform.Translate(0, speed, 0);
            rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
            sr.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            // transform.Translate(0, -speed, 0);
            rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
            sr.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Health")
        {
            if (health <= 90)
                health += 10;
            healthText.text = "Health: " + health + "%";
            audioSource.volume = 1f;
            audioSource.PlayOneShot(itemSound);

            // Debug.Log("Health: " + health);

            Destroy(collision.gameObject);

        }
        else if (collision.tag == "Score")
        {
            score += 10;
            scoreText.text = "Score: " + score;
            audioSource.volume = 1f;
            audioSource.PlayOneShot(itemSound);
            // Debug.Log("Score: " + score);
            Destroy(collision.gameObject);

        }
        else if (collision.tag == "Enemy")
        {
            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                health -= 10;
                healthText.text = "Health: " + health + "%";


            }

            // Debug.Log("Health: " + health);

        }

    }
}
