using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonClick;

    public void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void hFPlay()
    {
        buttonClick.Play();
        SceneManager.LoadScene (1);
    }

    public void vFPlay()
    {
        buttonClick.Play();
        SceneManager.LoadScene (2);
    }
}
