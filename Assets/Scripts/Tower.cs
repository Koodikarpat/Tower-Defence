using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    public int health = 100;
    public Text healthText;
    private bool gameEnded=false;
    public GameObject gameManager;

    void Update()
    {
        healthText.text = "Health: " + health.ToString();
        if (gameEnded)
        {
            return;
        }
    }

    public void TakeDamage(int amount)
    {
        if (health > 0)
        {
            health -= amount;
            if (health <= 0)
            {
                gameManager.GetComponent<GameManagerScript>().EndGame();
            }
        }
    }
    
}
