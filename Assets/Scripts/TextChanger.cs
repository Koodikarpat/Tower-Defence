using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {

    private Text TurretTextBox;

	// Use this for initialization
	void Start ()
    {
        TurretTextBox = GetComponent<Text>();
        gameObject.SetActive(false);		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeText(int id)
    {
        TurretBase TurretScript = BuildManager.instance.standardTurretPrefab.GetComponent<TurretBase>();
        if (id == 0)
        {
            TurretTextBox.text = "DAMAGE: " + "\nFIRERATE: " + TurretScript.fireRate.ToString() + "\nRANGE: " + TurretScript.range.ToString();
        }
    }
}
