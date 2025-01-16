using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent unityEvent;
    public GameObject isTrigger;

    public bool enteredForTheFirstTime = true;



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == isTrigger)
        {
            if (enteredForTheFirstTime)
            {
                unityEvent.Invoke();
                enteredForTheFirstTime = false;

            }
        }

    }
}
