using UnityEngine;

public class SoapBoxController : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for the path
    public float speed = 5f; // Speed of the car
    public float rotationSpeed = 5f; // Smooth rotation
    public Transform[] wheels; // Array of wheel transforms to rotate
    public float wheelRotationSpeed = 100f; // Speed of wheel rotation

    private int currentWaypointIndex = 0;

    void Update()
    {
        // Move the car towards the next waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);

        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Rotate towards the target
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        if(targetWaypoint.gameObject.tag != null && targetWaypoint.gameObject.tag =="TELEPORTATION_WAYPOINT"){ //TELEPORTATION_WAYPOINT
            transform.position = targetWaypoint.position;
        } 

        // Check if the car reached the current waypoint
        if (distance < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop to the first waypoint
        }

        // Rotate the wheels
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right * wheelRotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
