using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public ColumnSpawner kolonSpawn { get; set; }

    public Bird bird { get; set; }

    public BoxCollider2D KolonCollider;

    public float hareketHizi;
    public float yMax;
    public float yMin;

    public float randomY;

    public int yokOlSüre;

    void Start()
    {
        kolonSpawn = FindObjectOfType<ColumnSpawner>();
        bird = FindObjectOfType<Bird>();

        randomY = Random.Range (yMin, yMax);

        transform.position = new Vector3 (transform.position.x, randomY, transform.position.z);

        Destroy(gameObject, yokOlSüre);
    }

    void Update()
    {
        if (bird.skor >= 0 && bird.skor < 100)
        {
            hareketHizi = 4f;
        }

        if (bird.skor >= 100 && bird.skor < 200)
        {
            hareketHizi = 5f;
        }

        if (bird.skor >= 200)
        {
            hareketHizi = 6f;
        }

        if (bird.öldün)
        {
            KolonCollider.enabled = false;
        }
    }

    void FixedUpdate()
    {
        Kolon_Hareket();
    }

    void Kolon_Hareket()
    {
        if (!bird.öldün && kolonSpawn.kolonÜret)
        {
            transform.position -= new Vector3(hareketHizi * Time.deltaTime, 0, 0);
        }
    }
}
