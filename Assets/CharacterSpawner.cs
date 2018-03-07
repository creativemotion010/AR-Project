using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] CharacterPrefabs;
    public Transform[] SpawnPoints;
    public static CharacterSpawner Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            GameObject character = Instantiate(CharacterPrefabs[Random.Range(0, CharacterPrefabs.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position, Quaternion.identity);

            character.transform.parent = transform;
        }
    }
}