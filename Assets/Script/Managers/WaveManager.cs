using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Slider waveSlider;
    public Wave[] waves;
    public Transform[] enemySpawnPoints;
    public float timeBetweenWave;
    private float timer;

    private int maxEnemy = 0;
    private Wave currentWave;
    private int currentWaveIndex;
    private bool canSpawnEnemy = true;
    private float rateSpawnEnemy;

    public bool gameRunning = false;

    // Ubah dari script enemy factory di baris 63
    private EnemyFactory _poolEnemies;

    public void Initialize()
    {
        timer = timeBetweenWave;
        for (int i = 0; i < waves.Length; i++)
        {
            maxEnemy += waves[i].totalEnemyWave;
        }

        waveSlider.maxValue = maxEnemy;
        waveSlider.value = 0;
        _poolEnemies = GameObject.FindObjectOfType<EnemyFactory>();

    }
    void Update()
    {
        if (gameRunning == true)
        {
            currentWave = waves[currentWaveIndex];
            SpawnWave();
            //NextWave();
        }
    }

    void SpawnWave()
    {
        if (canSpawnEnemy && rateSpawnEnemy < Time.time)
        {
            //  Spawn Enemy dari enemy point
            Transform randomPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];

            for (int i = 0; i < _poolEnemies.listEnemies.Count; i++)
            {
                if (_poolEnemies.listEnemies[i].activeInHierarchy == false)
                {
                    _poolEnemies.listEnemies[i].SetActive(true);
                    _poolEnemies.listEnemies[i].transform.position = randomPoint.position;
                    _poolEnemies.listEnemies[i].transform.rotation = Quaternion.identity;
                    currentWave.totalEnemyWave--;

                    waveSlider.value++;

                    break;
                }
            }

            // Membuat interval antar spawn
            rateSpawnEnemy = Time.time + currentWave.spawnInterval;
            if (currentWave.totalEnemyWave == 0)
            {
                canSpawnEnemy = false;
                
            }


            


            Debug.Log("Wave : " + currentWave.waveName);
        }


        int countEnemisNow = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (countEnemisNow <= 0 && !canSpawnEnemy && currentWaveIndex + 1 <= waves.Length)
        {
            gameRunning = false;

            
            SistemSoal.instance.CheckSoalPerWave();

            if (GameManager.Instance.isGameOver)
            {
                SistemSoal.instance.Gui_Soal.SetActive(false);
            }
            Debug.Log("panggil soal");
        }
    }


    public void NextWaveEdited()
    {
        GameObject[] countEnemisNow = GameObject.FindGameObjectsWithTag("Enemy");

        if (countEnemisNow.Length == 0 && !canSpawnEnemy && currentWaveIndex + 1 != waves.Length)
        {
           
            currentWaveIndex++;
            canSpawnEnemy = true;
            timer = timeBetweenWave;
            
        }
        if (countEnemisNow.Length == 0 && !canSpawnEnemy && currentWaveIndex + 1 == waves.Length)
        {
            Debug.Log("Wave Finished");
            GameManager.Instance.PlayerCondition(true);
            SistemSoal.instance.Gui_Soal.SetActive(false);
        }
    }

    void NextWave()
    {
        GameObject[] countEnemisNow = GameObject.FindGameObjectsWithTag("Enemy");

        if (countEnemisNow.Length == 0 && !canSpawnEnemy && currentWaveIndex + 1 != waves.Length  )
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer Delay : " + timer);

            if (timer <= 0f)
            {
                currentWaveIndex++;
                canSpawnEnemy = true;
                timer = timeBetweenWave;
            }
        }
        if (countEnemisNow.Length == 0 && !canSpawnEnemy && currentWaveIndex + 1 == waves.Length)
        {
            Debug.Log("Wave Finished");
            GameManager.Instance.PlayerCondition(true);
        }
    }
}

// Wave Attribute
[System.Serializable]
public class Wave
{
    public string waveName;
    public int totalEnemyWave;
    public float spawnInterval;
}
