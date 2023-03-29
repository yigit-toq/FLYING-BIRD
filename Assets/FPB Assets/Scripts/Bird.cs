using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float ziplamaKuvveti;

    public int skor;

    public int sonSkor;
    public int titresim; //1 true, 2 false

    private int tekYap;

    public bool zipla;
    public bool öldün;
    public bool butonDegdi;

    public Text skorText;
    public Text sonSkorText;

    public GameObject ölümPaneli;
    public GameObject beyazPanel;
    public GameObject titresimAktif;
    public GameObject titresimPasif;

    public AudioSource bgSong;
    public AudioSource flySong;
    public AudioSource gameOverSong;
    public AudioSource winSong;
    public AudioSource buttonClick;
    public AudioSource impactSound;

    public Rigidbody2D birdRigid { get; set; }
    public Animator birdAnim { get; set; }
    public Animator ölümEkraniAnim;
    public Animator beyazPanelAnim;

    void Start()
    {
        birdRigid = GetComponent<Rigidbody2D>();
        birdAnim = GetComponent<Animator>();

        int yeniSkor = PlayerPrefs.GetInt("sonSkor");
        int titresimNe = PlayerPrefs.GetInt("Titreşim"); 

        sonSkor = yeniSkor;
        titresim = titresimNe;

        if (titresim == 1)
        {
            titresimAktif.SetActive(true);
            titresimPasif.SetActive(false);
        }
        else
        {
            titresimPasif.SetActive(true);
            titresimAktif.SetActive(false);
        }

        skor = 0;
        tekYap = 0;

        zipla = false;
        öldün = false;

        ölümPaneli.SetActive(false);
        beyazPanel.SetActive(false);

        skorText.text = skor.ToString();

        birdRigid.gravityScale = 0;

        birdAnim.Play("Idle");
    }
  
    void Update()
    {
        Kontroller();

        if (öldün)
        {
            PlayerPrefs.SetInt("Titreşim", titresim);

            if (skor > sonSkor)
            {
                sonSkor = skor;
                PlayerPrefs.SetInt("sonSkor", sonSkor);
            }

            sonSkorText.text = sonSkor.ToString();
        }
    }

    void FixedUpdate()
    {
        if (zipla)
        {
            birdRigid.velocity = new Vector2 (birdRigid.velocity.x, ziplamaKuvveti);
            flySong.Play();
            zipla = false;
        }

        if (birdRigid.velocity.y < 0)
        {
            birdAnim.SetBool("uç", false);
        }

        transform.eulerAngles = new Vector3(0, 0, birdRigid.velocity.y * 3);
    }

    void Kontroller() 
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !öldün && !butonDegdi)
        {
            zipla = true;
            birdAnim.SetBool("uç", true);
            birdRigid.gravityScale = 3.5f;
        }
    }

    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.gameObject.tag == "paraKazan")
        {
            skor += 1;
            if (titresim == 1)
            {
                Handheld.Vibrate();
            }
            winSong.Play();
            skorText.text = skor.ToString();
        } 
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Kolon" || other.gameObject.tag == "Zemin")
        {
            bgSong.Stop();

            ölümPaneli.SetActive(true);

            birdAnim.SetTrigger("öldü");
            ölümEkraniAnim.SetTrigger("öldün");

            if (tekYap == 0)
            {
                impactSound.Play();
                gameOverSong.Play();
                beyazPanel.SetActive(true);
                beyazPanelAnim.SetTrigger("panel");
            }
            tekYap = 1;

            öldün = true;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
        buttonClick.Play();
    }

    public void ReplayDik()
    {
        SceneManager.LoadScene(2);
        buttonClick.Play();
    }

    public void MaınMenu()
    {
        SceneManager.LoadScene(0);
        buttonClick.Play();
    }

    public void Titresim ()
    {
        if (titresim == 1)
        {
            titresim = 2;
            titresimAktif.SetActive(false);
            titresimPasif.SetActive(true);
        }
        else
        {
           titresim = 1;
            titresimAktif.SetActive(true);
            titresimPasif.SetActive(false);
        }
    }

    public void butonEnter()
    {
        butonDegdi = true;
    }

    public void butonExit()
    {
        butonDegdi = false;
    }
}
