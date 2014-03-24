using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CircleFollowing : MonoBehaviour {

    Vector3 velocity;
    Vector3 force;
    float maxSpeed = 5.0f;
    float mass = 1.0f;
    int currentWaypoint = 0;

    List<Vector3> waypoints = new List<Vector3>();

    // (5 marks)

    private void CreatePath(int points, float radius)
    {
        float thetaInc = (Mathf.PI * 2.0f) / (float)points;
        for (int i = 0; i < points; i++)
        {
            float theta = thetaInc * i;
            Vector3 point = new Vector3();
            point.x = Mathf.Sin(theta) * radius;
            point.z = Mathf.Cos(theta) * radius;
            point.y = 0;
            waypoints.Add(point);
        }
    }

    private void DrawPath()
    {
        for (int i = 1; i < waypoints.Count; i++)
        {
            Debug.DrawLine(waypoints[i - 1], waypoints[i], Color.red);
        }
        Debug.DrawLine(waypoints[waypoints.Count - 1], waypoints[0], Color.red);
    }

    Vector3 Seek(Vector3 point)
    {
        Vector3 desired = point - transform.position;
        desired.Normalize();

        desired *= maxSpeed;
        return desired - velocity;
    }

    Vector3 FollowPath()
    {
        float close = 1.0f;
        Vector3 toNext = waypoints[currentWaypoint] - transform.position;
        if (toNext.magnitude < close)
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Count)
            {
                currentWaypoint = 0;
            }
        }
        return Seek(waypoints[currentWaypoint]);
    }


	// Use this for initialization
	void Start () {
        CreatePath(10, 10);
        renderer.material.color = Color.black;
        transform.position = waypoints[0];
	}
	
	// Update is called once per frame
	void Update () {

        force = FollowPath();
        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;
        transform.position = transform.position + velocity * Time.deltaTime;

        DrawPath();
	}
}
