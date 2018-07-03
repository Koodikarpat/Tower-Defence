using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonball : ProjectileBase {

    public GameObject poisonCircle;

	// Use this for initialization
	void Start () {
        poisonCircle.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}	

	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            //Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            //poisonCircle.GetComponent<poisonExpand>().debuffer();
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    public override void HitTarget()
    {
        Debug.Log("hit");
        GameObject poisonCircleGO = (GameObject)Instantiate(poisonCircle, transform.position, transform.rotation);
        Destroy(poisonCircleGO, 2f);
        target.gameObject.GetComponent<Enemy>().takeDamage(damage);
    }
}
