using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureCharacter : MonoBehaviour
{
    public Transform TargetWayPoint;
    public float speed;
    public int targetPointIndex;
    public int trackCount = 0;

    private void Start()
    {
        TargetWayPoint = WayPoints.Instance.Points[1];
        //GetTargetWayPoint();
    }
    private void Update()
    {
        if (trackCount == WayPoints.Instance.Points.Length)
            Destroy(gameObject);

        if (transform.position == TargetWayPoint.position)
        {
            trackCount++;
            print("Arrived");
            GetTargetWayPoint();
            TargetWayPoint = WayPoints.Instance.Points[targetPointIndex];
        }

        transform.position = Vector3.MoveTowards(transform.position, TargetWayPoint.position, speed);
        transform.LookAt(TargetWayPoint.position);
    }
    private void GetTargetWayPoint()
    {
        for (int i = 0; i < WayPoints.Instance.Points.Length; i++)
        {
            if (TargetWayPoint == WayPoints.Instance.Points[i])
            {
                targetPointIndex = i + 1;
            }
        }
    }
}