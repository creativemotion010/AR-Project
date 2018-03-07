using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public Transform[] Points;

    public static WayPoints Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }
}
