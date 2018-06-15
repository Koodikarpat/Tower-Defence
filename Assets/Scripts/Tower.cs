using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    public int health = 100;
    public Text healthText;
    private bool gameEnded = false;

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
                EndGame();
            }
        }
    }
    void EndGame()
    {
        gameEnded = true;
    }
}
