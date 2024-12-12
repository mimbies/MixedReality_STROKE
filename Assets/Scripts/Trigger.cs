using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent unityEvent;
    public GameObject isTrigger;



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == isTrigger)
        {
            unityEvent.Invoke();
        }

    }
}
