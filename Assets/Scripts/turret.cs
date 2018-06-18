using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {

    public Transform target;
    public Sprite[] sprites;
    private SpriteRenderer renderer;

    [Header("Unity setup Fields")]

    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public int cost;

    public GameObject bulletPrefab;
    public Transform firePoint;

    // Use this for initialization
    void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update() {
        if (target == null)
            return;

        if (target.position.x > transform.position.x)
        {
            renderer.sprite = sprites[2];
        }
        else
        {
            renderer.sprite = sprites[1];
        }

        //Vector3 dir = target.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.forward);
        //Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //partToRotate.rotation = Quaternion.Euler(0f, 0, rotation.z);

        //partToRotate.LookAt(target, Vector3.forward);

        //RaycastHit hitTarget;
        //Vector3 aimvector = new Vector3(transform.position.x, target.position.y, transform.position.z);
        //bool hit = Physics.Raycast(aimvector, partToRotate.transform.forward, out hitTarget);
        if (fireCountdown <= 0f)
        { 
            shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

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
