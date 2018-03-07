using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    Apple,
    Orange,
    Pineapple
}
public class Fruit : MonoBehaviour
{
    public FruitType type;
    [HideInInspector]
    public Vector3 InitialPosition;
    private Vector3 fingerPosition;
    private Vector3 objectScreenPoint;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InitialPosition = transform.position;
    }

    public float timer;
    public float forceX;
    private void OnMouseUp()
    {
        endMouseX = Input.mousePosition.x;
        forceX = (endMouseX - startMouseX) * 200;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(new Vector3(forceX, 4000 * 1 / timer, 100000 * 1 / timer));
    }
    private float force;
    public float startMouseX, endMouseX;

    private void OnMouseDown()
    {
        startMouseX = Input.mousePosition.x;
    }
    void OnMouseDrag()
    {
        timer += Time.deltaTime;
    }
}