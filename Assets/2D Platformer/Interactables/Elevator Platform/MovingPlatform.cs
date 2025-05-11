using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float platformSpeed = 1f;

    private Transform CurrentPoint;

    void Awake()
    {

        CurrentPoint = pointA.transform; // Start at point A
    }

    void Update()
    {

        // Move towards the current point
        Vector2 direction = (CurrentPoint.position - transform.position).normalized;
        transform.position += (Vector3)(direction * (platformSpeed * Time.deltaTime));

        // Check if the platform has reached the current point
        if (Vector2.Distance(transform.position, CurrentPoint.position) < 0.1f)
        {
            // Switch to the other point
            CurrentPoint = (CurrentPoint == pointA.transform) ? pointB.transform : pointA.transform;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, .5f);
        Gizmos.DrawWireSphere(pointB.transform.position, .5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}