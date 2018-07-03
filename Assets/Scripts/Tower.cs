using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    public int health = 100;
    public Text healthText;
    private bool gameEnded=false;
    public GameObject gameManager;
    public GameObject healthLose;

    public GameObject canvas;

    List<HealthLoseText> textBoxes = new List<HealthLoseText>();


    void Update()
    {
        healthText.text = health.ToString();
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
            GameObject textTime = (GameObject)Instantiate(healthLose, transform.position + new Vector3(90, 0), Quaternion.identity, canvas.transform);

            Text healthLoseText = textTime.gameObject.GetComponent<Text>();

            if (textBoxes.Count > 0)

            {

                foreach (HealthLoseText text in textBoxes)

                {

                    if (text != null)

                    {

                        text.TextPositionChange(Vector3.up * 20);

                    }

                }

            }

            textBoxes.Add(textTime.gameObject.GetComponent<HealthLoseText>());

            healthLoseText.text = "-" + amount;

            Destroy(textTime, 5f);

            if (health <= 0)
            {
                gameManager.GetComponent<GameManagerScript>().EndGame();
            }
        }
    }
    
}
