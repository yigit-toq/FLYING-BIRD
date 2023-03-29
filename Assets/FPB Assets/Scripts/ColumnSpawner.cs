using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public Bird bird { get; set; }

    public bool kolonÜret;

    public float kolonSüre;
    public float süre;

    public GameObject kolon;

    void Start ()
    {
        bird = FindObjectOfType<Bird>();

        kolonÜret = false;

        süre = 0;
    }

    void Update ()
    {
        if (bird.birdRigid.gravityScale == 3.5f)
        {
            kolonÜret = true;
        }
        else
        {
            kolonÜret = false;
        }
    }

    void FixedUpdate()
    {
        if (kolonSüre >= süre && !bird.öldün && kolonÜret)
        {
            if (bird.skor >= 0 && bird.skor < 100)
            {
                Instantiate(kolon, transform.position, Quaternion.identity);
                süre = Random.Range(75, 100);
                kolonSüre = 0;
            }
            else if (bird.skor < 200)
            {
                Instantiate(kolon, transform.position, Quaternion.identity);
                süre = Random.Range(50, 75);
                kolonSüre = 0;
            }
            else
            {
                Instantiate(kolon, transform.position, Quaternion.identity);
                süre = Random.Range(30, 65);
                kolonSüre = 0;
            }
        }
        else if (kolonÜret)
        {
            kolonSüre++;
        }
    }
}
