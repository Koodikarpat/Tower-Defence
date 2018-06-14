using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPreFab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2.5f;
    private int waveIndex = 1;
    public Text waveCountdownText;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -=Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        for(int i=0; i<waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.4f);
        }
        waveIndex++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPreFab, spawnPoint.position, spawnPoint.rotation);
    }

    }
