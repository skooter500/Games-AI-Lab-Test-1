using UnityEngine;
using System.Collections;

public class CubeSteering : MonoBehaviour {

    Vector3 velocity;
    Vector3 force;
    float maxSpeed = 5.0f;
    float mass = 1.0f;
    Vector3 target;
    
    Vector3 ArriveAtTarget()
    {
        Vector3 toTarget = target - transform.position;
        float distance = toTarget.magnitude;
        if (distance < 1.0f)
        {
            target = Random.insideUnitSphere * 10.0f;
            return Vector3.zero;
        }
        else
        {
            float slowing = 10.0f;
            float ramped = maxSpeed * (distance / slowing);
            float clamped = Mathf.Min(ramped, maxSpeed);
            Vector3 desired = clamped * (toTarget / distance);
            return desired - velocity;
        }
    }

    
	// Use this for initialization
	void Start () {
        target = Random.insideUnitSphere * 10.0f;            
	}
	
	// Update is called once per frame
	void Update () 
    {
        force = ArriveAtTarget();
        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.position = transform.position + velocity * Time.deltaTime;

        Debug.DrawLine(transform.position, target, Color.blue);
	}
}
