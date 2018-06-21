using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum direction { 1, 2, 3}

public class waypoint : MonoBehaviour {

    

    public GameObject[] neighbors;
    public GameObject nextwaypoint;

    int counter = 0;

	// Use this for initialization
	void Start () {
        if (neighbors.Length > 0) nextwaypoint = neighbors[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public Transform getwaypoint()
    {
        if (neighbors.Length >= 2) {
            counter += 1;
            nextwaypoint = neighbors[1];
            if (counter > 1)
            {
                counter = 0;
                nextwaypoint = neighbors[0];
            }
            
        }
        

        return nextwaypoint.transform;

    }
}
