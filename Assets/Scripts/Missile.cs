using System;
using System.Collections;
using UnityEngine;

public class Missile : ProjectileBase
{

    public float explosionRadius = 0f;
    public GameObject MissileAudio;

    // Update is called once per frame
    void Update()
    {
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
        transform.LookAt(target);
    }

   public override void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(effectIns, 0.3f);
        target.gameObject.GetComponent<Enemy>().takeDamage(damage);
        Destroy(gameObject);

        GameObject MissileAud = (GameObject)Instantiate(MissileAudio, transform.position, transform.rotation);
        Destroy(MissileAud, 2f);

        if (explosionRadius > 0f)
        {
            Explode();
        }

        Destroy(gameObject);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().takeDamage(7);
            }
        }
    }
}