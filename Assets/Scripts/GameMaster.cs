using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public static int goldamount = 0;
    public Text money;
    public Text roundcount;
    public GameObject tower;
    int Starthealth;
    public static int Totalscore = 0;
    public Text Score;
    public Wave[] waves;
    bool BonusScore = false;
    public static int enemytype = 1;
    public static int Lives = 3;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown;
    public float firstCountDown = 2.5f;
    private int waveIndex = 0;
    public Text waveCountdownText;
    //public Text winningText;
    public Image timerBar;
    private bool isFirstCountDown = true;
    private bool isWaveOn = false;
    private bool isTimerOn = true;
    bool FastForwardOn = false;
    public GameObject speedButton;



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
        //winningText.GetComponent<Text>().enabled = false;
        countdown = firstCountDown;
        Totalscore = 0;
        Scoreupdate(0);
        goldamount = 600;
        goldupdate(0);
        waveCountdownText.GetComponent<Text>().enabled = true;
    }


    void Update()
    {

        //Livescounter.text = "Lives: " + Lives;
        //Debug.Log("enemies " + EnemiesAlive);
        if (isTimerOn && !isWaveOn)
        {
            waveCountdownText.GetComponent<Text>().enabled = true;

            timerBar.GetComponent<Image>().enabled = true;
        }
        else
        {
            waveCountdownText.GetComponent<Text>().enabled = false;

            timerBar.GetComponent<Image>().enabled = false;
        }

        if (EnemiesAlive <= 0 && !isFirstCountDown)
        {
            isWaveOn = false;
        }
        if (countdown <= 0f && !isWaveOn)
        {
            isFirstCountDown = false;
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }
        else
        {
            if (Starthealth == tower.GetComponent<Tower>().health && !BonusScore)
            {
                Scoreupdate(5);
                BonusScore = true;
            }
            if (waveIndex == waves.Length && EnemiesAlive == 0)
            {
                isTimerOn = false;
                GameWon();
                this.enabled = false;
            }

        }

        if (!isWaveOn)
        {
            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            waveCountdownText.text = Mathf.Round(countdown).ToString();
        }


        if (isFirstCountDown)
        {
            timerBar.fillAmount = countdown / firstCountDown;
        }
        else
        {
            timerBar.fillAmount = countdown / timeBetweenWaves;
        }

        if (waveIndex <= waves.Length)
        {
            roundcount.text = "Waves: " + (waveIndex) + "/" + waves.Length;
        }

    }

    IEnumerator SpawnWave()
    {
        isWaveOn = true;
        Wave wave = waves[waveIndex];
        EnemiesAlive = 0;
        foreach (int amount in wave.count)
        {
            Starthealth = tower.GetComponent<Tower>().health;
            BonusScore = false;




            foreach (int total in wave.count)
            {
                EnemiesAlive += total;
            }





            int totalenemies = EnemiesAlive;

            waveIndex++;

            while (totalenemies > 0)
            {
                enemytype = Random.Range(0, wave.enemy.Length);

                if (wave.count[enemytype] > 0)
                {
                    SpawnEnemy(wave.enemy[enemytype]);

                    wave.count[enemytype]--;

                    totalenemies--;

                    yield return new WaitForSeconds(1f / wave.rate);
                }
            }
        }


    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
    public void Scoreupdate(int amount)
    {
        Totalscore += amount;
        Score.text = "Score :" + Totalscore;
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
        //winningText.GetComponent<Text>().enabled = true;
    }

    public void FastForward()
    {
        FastForwardOn = !FastForwardOn;
        if (FastForwardOn)
        {
            speedButton.GetComponentInChildren<Text>().text = "1x speed";
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 3;
            speedButton.GetComponentInChildren<Text>().text = "3x speed";
        }
    }
}
