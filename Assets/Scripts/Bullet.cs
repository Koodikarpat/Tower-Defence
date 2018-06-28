using System;
using System.Collections;
using UnityEngine;

public class Bullet : ProjectileBase {

  

   
	
	// Update is called once per frame
	void Update () {
		if (target == null)
        {
            
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}
    
    
}
