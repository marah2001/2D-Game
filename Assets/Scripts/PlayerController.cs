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
    public int score;
    bool isJump;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip itemSound;
    public AudioClip jumpSound;
    Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        score = 0;
        isJump = false;
        healthText.text = "Health: " + health + "%";
        scoreText.text = "Score: 0";
        tr = GetComponent<Transform>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
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
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rb.velocity = new Vector2(0, jump); // jump
            isJump = true;
            // rb.AddForce(new Vector2(0, jump));
            if (Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                isJump = false;
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // if (Input.GetAxis("Jump") > 0)
        // {
        //     rb.velocity = new Vector2(0, jump);
        //     audioSource.volume = 0.5f;
        //     audioSource.PlayOneShot(jumpSound);

        // }
        // anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // else if (Input.GetAxis("Horizontal") < 0)
        // {
        //     transform.Translate(-speed, 0, 0);
        //     sr.flipX = true;
        // }

    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        // if (Input.GetAxis("Horizontal") > 0)
        // {
        //     // transform.Translate(0, speed, 0);
        //     rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        //     sr.flipX = false;
        // }
        // else if (Input.GetAxis("Horizontal") < 0)
        // {
        //     // transform.Translate(0, -speed, 0);
        //     rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        //     sr.flipX = true;
        // }
        // // if (Input.GetButtonDown("Jump"))
        // // {
        // //     rb.velocity = new Vector2(0, jump); // jump
        // //     // rb.AddForce(new Vector2(0, jump));
        // // }
        //   anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
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
        else if (collision.tag == "Gem")
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
            if (isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                health -= 1;
            }
            if (transform.position.y > collision.transform.position.y)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                health -= 10;
                healthText.text = "Health: " + health + "%";

                if (health <= 0)
                {
                    Debug.Log("Game Over");
                    // Destroy(gameObject);
                }

                // Debug.Log("Health: " + health);

            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);

            }
            else
            {
                health -= 1;
            }
        }
    }
}
