using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    public UnityEvent trashIsThrownAway;
    public GameObject trashTrigger;



    public void OnTriggerEnter(Collider trashCube)
    {
        if (trashCube.gameObject == trashTrigger)
        {
            trashIsThrownAway.Invoke();
        }

    }
}
