using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent trashIsThrownAway;
    public GameObject isTrigger;



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == isTrigger)
        {
            trashIsThrownAway.Invoke();
        }

    }
}
