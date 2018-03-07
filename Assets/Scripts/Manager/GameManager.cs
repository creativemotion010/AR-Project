using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Main,
    RealEstate,
    Product,
    Enivornment
}

public class GameManager : MonoBehaviour
{
    public GameObject SectionsMenu;
    public GameState gameState = new GameState();
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        SceneController.Instance.StartScene(0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        switch (scene.name)
        {
            case "Main":
                gameState = GameState.Main;
                break;
            case "RealEstate":
                gameState = GameState.RealEstate;
                break;
            case "Product":
                gameState = GameState.Product;
                break;
            case "Environment":
                gameState = GameState.Enivornment;
                break;
            default:
                break;
        }

        if (gameState != GameState.Main)
        {
            SectionsMenu.SetActive(true);
        }
        else
        {
            SectionsMenu.SetActive(false);
        }
    }
}
