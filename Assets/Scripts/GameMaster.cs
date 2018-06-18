using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static int EnemiesAlive = 0;

 
    public static int goldamount = 0;
    public Text money;
    public Text roundcount;
    public Text Livescounter;

    public Wave[] waves;

    public static int Rounds = 0;

    public static int Lives = 3;
 
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2.5f;
    public int waveIndex = 0;
    public Text waveCountdownText;

    public static GameMaster instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //Awake is always called before any Start functions
    //void Awake()
    //{

    //    goldupdate(65);
    //    //Check if instance already exists
    //    if (instance == null)

    //        //if not, set instance to this
    //        instance = this;

    //    //If instance already exists and it's not this:
    //    else if (instance != this)

    //        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
    //        Destroy(gameObject);

    //    //Sets this to not be destroyed when reloading scene
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Start()
    {
        goldupdate(65);
    }


    void Update()
    {

        Livescounter.text = "Lives: " + Lives;

        if (EnemiesAlive > 0)
        {

            return;

        }
        else
        {
            if (waveIndex == waves.Length)
            {
                
                GameWon();
                this.enabled = false;
            }
        }
        if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
                return;
            }
        
        countdown -=Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = Mathf.Round(countdown).ToString();





        if (waveIndex < waves.Length)
        {
            roundcount.text = "Waves: " + (waveIndex + 1) + "/" + waves.Length;
        }

    }

    IEnumerator SpawnWave()
    {
            Rounds++;
            Wave wave = waves[waveIndex];

            EnemiesAlive = wave.count;
            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }

            waveIndex++;


    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    public void goldupdate(int amount)
    {
    goldamount += amount;
        money.text = "$" + goldamount;
        

    }

    void GameLost()
    {
        Debug.Log("LEVEL LOST");
    }

    void GameWon()
    {
        Debug.Log("LEVEL WON");
    }
}