using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public Bird bird { get; set; }

    public float hiz;

    public float sinirNokta;
    public float baslangicNokta;

    void Start()
    {
        bird = FindObjectOfType<Bird>();
    }

    void Update ()
    {
        if (bird.skor >=0 && bird.skor < 100)
        {
            hiz = 4f;
        }

        if (bird.skor >= 100 && bird.skor < 200)
        {
            hiz = 5f;
        }

        if (bird.skor >= 200)
        {
            hiz = 6f;
        }
    }

    void FixedUpdate()
    {
        if (!bird.öldün)
        {
            transform.position -= new Vector3(hiz * Time.fixedDeltaTime, 0, 0);
        }

        if (transform.position.x <= sinirNokta)
        {
            transform.position = new Vector3 (baslangicNokta, transform.position.y, transform.position.z);
        }
    }
}
