﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeControl : MonoBehaviour
{

    public GameObject fadeEffect;

    public VoiceRecognition voiceRecognition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(voiceRecognition.sendAmbulance){
            fadeEffect.SetActive(true);
            }
        
    }
}
