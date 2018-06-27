using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {

    public Transform target;
    public Sprite[] sprites;
    private SpriteRenderer spriterender;

    [Header("Unity setup Fields")]

    public float range = 15f;
    public int damageovertime = 15;
    private Enemy targetenemy;
    public bool uselaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impacteffect;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public int cost;
    public GameObject bulletPrefab;
    public Transform firePoint;
   
    void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        spriterender = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetenemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }
    }

    void Update() {
        if (target == null)
        {
            if (uselaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impacteffect.Stop();
                }
                    
            }       
            return;
        }

       //if (target.position.x > transform.position.x)
       // {
       //     spriterender.sprite = sprites[2];
       // }
       // else
       // {
       //     spriterender.sprite = sprites[1];
       // }


        lockontarget();
    
        if (uselaser)
        {
            laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
     
    }
     void lockontarget()

    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
       partToRotate.rotation = Quaternion.Euler(0f, 0, rotation.z);
    }
  
    void laser()

    {

        targetenemy.takeDamage((int)(damageovertime * Time.deltaTime));


        if (!lineRenderer.enabled)
        {
           lineRenderer.enabled = true;
            impacteffect.Play();
        }
                
 
        lineRenderer.SetPosition(1, firePoint.position);
        lineRenderer.SetPosition(0, target.position);

        impacteffect.transform.position = target.position;

    }
  
    void shoot()


    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletGO.transform.rotation= Quaternion.Euler(0, 0, 0);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {

         Gizmos.DrawWireSphere(transform.position, range);
    }


}
