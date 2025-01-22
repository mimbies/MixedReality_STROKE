using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    public GameObject objectToMove;
    public VoiceRecognition voiceRecognition;
    Animator anim;


    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        //Move();

        if (voiceRecognition.juergenHasBeenGreeted)
        {
            anim.SetBool("juergenHasBeenGreetedAnim", true);
        }


        if (voiceRecognition.sendAmbulance)
        {
            anim.SetBool("sendAmbulance", true);
        }

        if (voiceRecognition.emilyStandsUp)
        {
            anim.SetBool("emilyStandsUpAnim", true);
        }

        if (voiceRecognition.emilyExtendsArms)
        {
            anim.SetBool("emilyRaisesArmAnim", true);
        }

        if(voiceRecognition.emilyOneLeg){
            anim.SetBool("emilyOneLegAnim", true);
        }




    }

    /*public void Move()
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
    }*/
}


