using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public float speed;

    public float startSpeed=10;
    private float slowTime;
    public float startSlowTime=4f;
    private bool loseHealth = false;
    public int damage = 20;
    private Transform target;
    public GameObject targetTower;
    private int wavepointIndex = 0;
    public int startinghealth;
    public Image healthbar;
    private float health;
    private float distance;
    private float travelledDistance=0;
    private Vector3 previousPosition;
    public GameObject gameMaster;
    public GameObject DeathIncome;
    public int reward;

    void Start()
    {
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

        if (slowTime <= 0f)
        {
            speed = startSpeed;
            slowTime =startSlowTime;
        }
        if (loseHealth==true)
        {
            slowTime -= Time.deltaTime;
        }

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
    
        if ( health <= 0)
        {
            gameMaster.GetComponent<GameMaster>().goldupdate(reward);
            GameObject TextTime = (GameObject)Instantiate(DeathIncome, transform.position, Quaternion.identity);
            Text DeathIncomeOT = TextTime.gameObject.GetComponentInChildren<Text>();
            DeathIncomeOT.text = "+$" + reward;
            Destroy(TextTime, 1.5f);
            Destroy(gameObject);
            GameMaster.EnemiesAlive--;
        }

    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Damage ()
    {
        targetTower.GetComponent<Tower>().TakeDamage(damage);
    }

}

