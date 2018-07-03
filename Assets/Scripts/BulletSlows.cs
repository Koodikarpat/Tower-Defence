using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSlows : ProjectileBase {

    public float slowAmount = 0.1f;

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

    }

    public override void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        target.gameObject.GetComponent<Enemy>().takeDamage(damage);
        target.gameObject.GetComponent<Enemy>().Slow(slowAmount);
        Destroy(gameObject);

    }
}
