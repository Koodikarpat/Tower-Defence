using System;
using System.Collections;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public int Damage = 4;
    public GameObject MissileAudio;

    public void Seek(Transform _target)
    {
        target = _target;
    }

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

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        target.gameObject.GetComponent<Enemy>().takeDamage(Damage);
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
                collider.GetComponent<Enemy>().takeDamage(5);
            }
        }
    }
}