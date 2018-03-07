using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public GameObject robotPrefab;
    public float timeToLand;
    public Transform[] spawnPoints;

    private void Start()
    {
        Invoke("StartLanding", 10);
    }

    private void StartLanding()
    {
        GetComponent<Animator>().SetTrigger("land");
        StartCoroutine(SpawnRobots());
    }

    private IEnumerator SpawnRobots()
    {
        yield return new WaitForSeconds(timeToLand);

        for (int i = 0; i < 5; i++)
        {
            GameObject robot = Instantiate(robotPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(10);
        }
    }
}