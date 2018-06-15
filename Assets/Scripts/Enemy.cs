using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform target;
    private int wavepointIndex = 0;
    private int startinghealth = 100;
    public Image healthbar;
    private float health;




    void Start()
    {
        target = Waypoints.points[0];
        health = startinghealth;
    }

    void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
        }


        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];

        //target = target.GetComponent<waypoint>().getwaypoint();
    }
    public void takeDamage(int damage)
    {
        health -= damage;

        healthbar.fillAmount = health / startinghealth;
    
        if ( health <= 0) {
            GameMaster.instance.goldupdate(15);
            Destroy(gameObject);
        }
    }
}

