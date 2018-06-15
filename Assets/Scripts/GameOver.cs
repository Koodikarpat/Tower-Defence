﻿using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public Text roundsText;

    void OnEnable()
    {
        roundsText.text = (GameManagerScript.Rounds-1).ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
	
}
