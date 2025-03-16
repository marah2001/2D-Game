using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    [SerializeField]
    Transform player;
    SpriteRenderer sr;
    Vector3 startPos;
    [SerializeField]
    float eagleHeight = 1;
    [SerializeField]
    float eagleSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPos = transform.position;
        StartCoroutine(EagleAnimation());

    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

    }
    IEnumerator EagleAnimation()
    {
        Vector3 endPos = new Vector3(startPos.x, startPos.y + eagleHeight, startPos.z);
        bool isFlighing = true;
        float value = 0;
        while (true)
        {
            yield return null;
            if (isFlighing)
            {
                transform.position = Vector3.Lerp(startPos, endPos, value);
            }
            else
            {
                transform.position = Vector3.Lerp(endPos, startPos, value);
            }
            value += Time.deltaTime * eagleSpeed;
            if (value > 1)
            {
                value = 0;
                isFlighing = !isFlighing;
            }
        }

    }
}
