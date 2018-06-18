using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    private bool gameEnded = false;
    public GameObject gameOverUI;
    public static int Rounds;
	

	void Update ()
    {
        if (gameEnded)
            return;
        		
	}

    public void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

}
