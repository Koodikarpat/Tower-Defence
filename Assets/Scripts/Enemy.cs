using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public int damage = 20;
    private Transform target;
    public GameObject targetTower;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
        targetTower = GameObject.Find("Tower");
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex>=Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            Damage();
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void Damage ()
    {
        targetTower.GetComponent<Tower>().TakeDamage(damage);
    }

}
