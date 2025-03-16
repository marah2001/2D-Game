using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool isRight = true;
    [SerializeField]
    float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) <= 0.01f)
        {
            isRight = !isRight;
            sr.flipX = isRight;
            rb.velocity = new Vector2(isRight ? 1 : -1, 0);
        }
        if (isRight)
            rb.velocity = new Vector2(Time.fixedTime * speed, rb.velocity.y);


        else
            rb.velocity = new Vector2(Time.fixedTime * speed * -1, rb.velocity.y);

    }
}
