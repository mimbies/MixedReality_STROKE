using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundWorldAxis : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 rotateSnapZones = new Vector3(25.959f, -3.911f, 217.6568f);


    void Start()
    {
        transform.Rotate(0, 0, -90, Space.Self);
        transform.position = rotateSnapZones;

    }

    // Update is called once per frame
    void Update()
    {


    }
}
