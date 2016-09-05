using UnityEngine;
using System.Collections;

public class MoveComponent : AComponent {

    //public float stoppingDistance;
    public float speed;

    public void MoveTo(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
