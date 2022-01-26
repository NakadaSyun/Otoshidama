﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource BGMS;
    private bool Bgmfig;
    // Start is called before the first frame update
    void Start()
    {
        BGMS.Play();
        Bgmfig = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0 && !Bgmfig)
        {
            BGMS.volume = BGMS.volume / 2;
            Bgmfig = true;
        }
        else if (Time.timeScale != 0 && Bgmfig)
        {
            BGMS.volume = BGMS.volume*2;
            Bgmfig = false;
        }
        
    }
}
