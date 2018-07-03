using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour {

    protected float fireCountdown;
    public bool isUpgraded = false;
    protected string enemyTag = "Enemy";
    protected Transform target;
    protected GameObject gameMaster;

    [Header("Unity setup Fields")]

    public float range;
    public float fireRate;
    public int cost;
    public int damageT;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject Circle;

    public int GetSellAmount()
    {
        return cost / 2;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    protected void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGO.transform.rotation = Quaternion.Euler(0, 0, 0);

        ProjectileBase bullet = bulletGO.GetComponent<ProjectileBase>();
        bullet.damage = damageT;

        if (bullet != null)
            bullet.Seek(target);
    }

    public void UpgradeTurret()
    {
        range += 10f;
        damageT += 10;
        isUpgraded = true;
    }

    public void SellTurret()
    {
        Destroy(gameObject);
        gameMaster.GetComponent<GameMaster>().goldupdate(GetSellAmount());
    }
    public void UpdateRange()
    {
        float Scale = range / transform.localScale.x * 2f;
        Circle.transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    public void ToggleRange()
    {
        if (Circle.gameObject.activeSelf)
        {
            Circle.SetActive(false);
        }
        else
        {
            Circle.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
