using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public GameObject objectToMove;
    public VoiceRecognition voiceRecognition;


    // Update is called once per frame
    public void Update()
    {
        Move();


    }

    public void Move()
    {


        if (voiceRecognition.juergenHasBeenGreeted)
        {

            objectToMove.transform.position += Vector3.up * Time.deltaTime;
            //Debug.Log(" y position: " + objectToMove.transform.position.y);

        }

        if (objectToMove.transform.position.y >= 10f)
        {
            voiceRecognition.juergenHasBeenGreeted = false;
            Debug.Log("y pos above 10. jürgen stops moving");
        }
    }
}


