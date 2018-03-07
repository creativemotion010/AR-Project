using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public GameObject particles;
    public AudioClip squeezeSound;
    [HideInInspector]
    public List<Fruit> SqueezedFruits;
    private Camera mainCamera;
    public static Bottle Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Contains("Fruit"))
        {
            SqueezedFruits.Add(other.gameObject.GetComponent<Fruit>());
            other.gameObject.SetActive(false);
            //mainCamera.GetComponent<AudioSource>().PlayOneShot(squeezeSound);
            particles.SetActive(true);
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().SetTrigger("Jump");
            Invoke("HideParticles", 0.8f);
            if (other.gameObject.tag.Contains("Apple"))
            {
                //ChangeMaterial();
            }
        }
    }

    void HideParticles()
    {
        particles.SetActive(false);
    }
}
