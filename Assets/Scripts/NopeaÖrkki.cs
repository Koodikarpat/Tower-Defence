using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NopeaÖrkki : MonoBehaviour {

    public float speed;

    public float startSpeed = 10;
    private float slowTime;
    public float startSlowTime = 4f;
    private bool loseHealth = false;
    public int damage = 20;
    private Transform target;
    public GameObject targetTower;
    private int wavepointIndex = 0;
    public int startinghealth = 100;
    public Image healthbar;
    private float health;
    private float distance;
    private float travelledDistance = 0;
    private Vector3 previousPosition;
    public GameObject gameMaster;
    Animator animator;

    Vector3 Lastposition;

    void Start()
    {
        Lastposition = transform.position;
        animator = GetComponent<Animator>();
        target = Waypoints.points[0];
        targetTower = GameObject.Find("Tower");
        previousPosition = transform.position;
        distance = Vector3.Distance(transform.position, target.position);
        health = startinghealth;
        gameMaster = GameObject.Find("GameMaster");
        speed = startSpeed;
        slowTime = startSlowTime;
    }

    void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime, Space.World);

        if (travelledDistance >= distance)
        {
            GetNextWaypoint();
            distance = Vector3.Distance(transform.position, target.position);
            travelledDistance = 0;
        }
        else
        {
            travelledDistance += Vector3.Distance(previousPosition, transform.position);
            previousPosition = transform.position;
        }

        if (Lastposition.x - transform.position.x < -0.2)
        {
            animator.SetInteger("direction", 3);
        }

        else if (Lastposition.x - transform.position.x > 0.2)
        {
            animator.SetInteger("direction", 1);
        }

        else if (Lastposition.y - transform.position.y < -0.2)
        {
            animator.SetInteger("direction", 2);
        }

        else if (Lastposition.y - transform.position.y > 0.2)
        {
            animator.SetInteger("direction", 0);
        }

        if (slowTime <= 0f)
        {
            speed = startSpeed;
            slowTime = startSlowTime;
        }
        if (loseHealth == true)
        {
            slowTime -= Time.deltaTime;
        }
        Lastposition = transform.position;
    }
    void GetNextWaypoint()
    {

        if (wavepointIndex <= Waypoints.points.Length - 2)
        {
            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
        }
        else
        {
            GameMaster.Lives--;
            Destroy(gameObject);
            Damage();
            GameMaster.EnemiesAlive--;
        }
        if (wavepointIndex < Waypoints.points.Length - 1)
        {
            //wavepointIndex++;
        }
        //target = target.GetComponent<waypoint>().getwaypoint();
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        loseHealth = true;
        slowTime = startSlowTime;
        healthbar.fillAmount = health / startinghealth;
 
        if (health <= 0)
        {
            gameMaster.GetComponent<GameMaster>().goldupdate(75);
            gameMaster.GetComponent<GameMaster>().Scoreupdate(1);
            Destroy(gameObject);
            GameMaster.EnemiesAlive--;
        }
    }
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }
    void Damage()
    {
        targetTower.GetComponent<Tower>().TakeDamage(damage);
    }
}
