using UnityEngine;
using System.Collections;

/**
 * Really simple camera which follow the player from a topdown view
 */
public class TopDownCamera : MonoBehaviour {

    // Target to follow
    public Transform target;
    // Offset to apply to the camera
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = target.position + offset;             
    }
}
