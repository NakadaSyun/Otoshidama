using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public AudioClip Top;
    public AudioClip On;
    public AudioSource titleS;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void StartButton()
    {
        titleS.PlayOneShot(On);
        SceneManager.LoadScene("ModeSelect");
    }

    public void MouseTop()
    {
        titleS.PlayOneShot(Top);
    }
}
