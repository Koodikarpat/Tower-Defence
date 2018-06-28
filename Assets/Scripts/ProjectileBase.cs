using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    protected Transform target;

    public float speed = 5f;
    public GameObject impactEffect;
    public int damage = 0;

    public void Seek(Transform _target)
    {
        target = _target;
    }
    public virtual void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        target.gameObject.GetComponent<Enemy>().takeDamage(damage);
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
