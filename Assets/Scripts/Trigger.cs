using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent unityEvent;
    public GameObject isTrigger;

    public bool enteredForTheFirstTime = true;

    public GameObject offset;

    public void Update()
    {
        if (offset != null)
        {
            float x = offset.transform.position.x;
            float z = offset.transform.position.z;
            offset.transform.position = new Vector3(x, 0.5f, z);
        }
    }



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
