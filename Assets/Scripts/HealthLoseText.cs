using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoseText : MonoBehaviour {
    private bool isMoveOn = false;
    Vector3 moveVector;
    Vector3 targetPosition;
    Vector3 direction;
    Vector3 startingPosition;
    float speed = 50;
    float distance;
    float travelledDistance;

	// Use this for initialization
	void Start () {
        transform.position = transform.position + new Vector3(-35, 5, 0);
        targetPosition = transform.position;
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoveOn && travelledDistance < distance)
        {
            travelledDistance += speed * Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime);
        }
	}

    public void TextPositionChange(Vector3 move)
    {
        isMoveOn = true;
        moveVector = move;
        targetPosition = targetPosition + move;
        direction = move.normalized;
        distance = (targetPosition - startingPosition).magnitude;
    }
}
