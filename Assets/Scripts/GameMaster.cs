using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public static int goldamount = 0;
    public Text money;

    public Transform enemyPreFab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown;
    private int waveIndex = 1;
    public Text waveCountdownText;
    public Image timerBar;
    private bool isFirstCountDown = true;
    public float firstCountDown = 2.5f;

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
        countdown = firstCountDown;
        goldamount = 600;
        goldupdate(0);
    }


    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            isFirstCountDown = false;
        }
        countdown -=Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
        if (isFirstCountDown)
        {
            timerBar.fillAmount = countdown / firstCountDown;
        }
        else
        {
            timerBar.fillAmount = countdown / timeBetweenWaves;
        }


        
    }

    IEnumerator SpawnWave()
    {
        for(int i=0; i<waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.4f);
        }
        waveIndex++;
        GameManagerScript.Rounds++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPreFab, spawnPoint.position, spawnPoint.rotation);
    }

    public void goldupdate(int amount)
    {
        goldamount += amount;
        money.text = "$" + goldamount;
        

    }
}