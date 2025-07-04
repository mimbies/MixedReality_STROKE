using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour
{

    public Image img;
    public Transform target;
    public TMP_Text meter;
    public Vector3 offset;

    public Transform newTarget;

    public bool enabledWaypoint = false;


    void Update()
    {


        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((((int)Vector3.Distance(target.position, transform.position))) / 2).ToString() + "m";



    }

    public void setNewTarget()
    {
        this.target = newTarget;
    }

    public void enableWaypoint()
    {
        if (!enabledWaypoint)
        {
            enabledWaypoint = true;
        }
    }

    public void disableWaypoint()
    {
        if (enabledWaypoint)
        {
            enabledWaypoint = false;
        }
    }
}
