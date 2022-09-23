using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance;
    void Awake() { instance = this; }

    //Enemy Prefabs
    public List<GameObject> prefabs;
    //Enemy Spawn Points
    public List<Transform> points;
    //Enemy Spawn Interval
    public bool isPlaying = true;

    public int maxEnemyCreated = 3;

    public float turnOffDelay = 5f;

    public float spawnInterval = 2f;
    public Transform pullObject;
    public List<Transform> spawnObject;

    // Nurul
    public List<GameObject> listEnemies;

    private int EnemyID;

    private void Start()
    {
        spawnEnemy();
    }

    public void startSpawning()
    {
        //
        //StartCoroutine(spawnDelay());

    }

    IEnumerator spawnDelay()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(spawnInterval);
            ActivateEnemy();
        }
    }

    void spawnEnemy()
    {
        for (int i = 0; i < maxEnemyCreated; i++)
        {
            int randomPrefabID = Random.Range(0, prefabs.Count);
            GameObject enemies = Instantiate(prefabs[randomPrefabID]);
            listEnemies.Add(enemies);
            enemies.transform.SetParent(pullObject);
            enemies.GetComponent<EnemyMovement>().pullObject = pullObject;
            enemies.SetActive(false);
        }
    }

    void ActivateEnemy()
    {
        int randomPrefabID = Random.Range(0, prefabs.Count);
        //
        int randomPointID = Random.Range(0, points.Count);

        spawnObject[EnemyID].transform.SetParent(points[randomPointID]);
        spawnObject[EnemyID].transform.localPosition = Vector2.zero;

        spawnObject[EnemyID].gameObject.SetActive(true);

        var enemy = spawnObject[EnemyID].GetComponent<EnemyMovement>();

        enemy.StartCoroutine(enemy.TurnOffEnemy(pullObject, turnOffDelay));


        if (EnemyID >= spawnObject.Count - 1)
        {
            EnemyID = 0;
        }
        else
        {
            EnemyID++;
        }
    }
}
