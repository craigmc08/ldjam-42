using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStateManager : MonoBehaviour {
    public GameObject GameUI;
    public GameObject GameOverUI;

    void Start()
    {
        GameUI.SetActive(false);
        GameOverUI.SetActive(false);
    }

    public void GameStart()
    {
        GameUI.SetActive(true);
        GameOverUI.SetActive(false);
    }
    public void GameOver()
    {
        GameUI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    void Update()
    {
        if (GameOverUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameController.StartGame();
            } else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
