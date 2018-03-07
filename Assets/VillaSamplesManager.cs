using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillaSamplesManager : MonoBehaviour {

    public static VillaSamplesManager Instance;
    // Use this for initialization

    public GameObject [] VillaSamples;
    public GameObject CurrentVilla;
    public GameObject CurrentRoof;
    public GameObject CurrentWindows;
    public GameObject CurrentWalls;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start () {
        CurrentVilla = VillaSamples[0];
        SelectCurrentSample(0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectCurrentSample(int inpt_sampleNum)
    {
        CurrentVilla = VillaSamples[inpt_sampleNum];

        foreach (Transform tr in CurrentVilla.transform)
        {
            switch (tr.tag)
            {
                case "roof":
                    CurrentRoof = tr.gameObject;
                    break;
                case "walls":
                    CurrentWalls = tr.gameObject;
                    break;
                case "windows":
                    CurrentWindows = tr.gameObject;
                    break;
            }
        }

    }
    public void OnOffShape(string typeOfshape)
    {
        if(CurrentVilla!=null)
        {
            switch(typeOfshape)
            {
                case "roof":
                    CurrentRoof.SetActive(!CurrentRoof.activeInHierarchy);
                    break;
                case "walls":
                    CurrentWalls.SetActive(!CurrentWalls.activeInHierarchy);
                    break;
                case "windows":
                    CurrentWindows.SetActive(!CurrentWindows.activeInHierarchy);
                    break;
            }
        }

    }

}
