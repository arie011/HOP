using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawned : MonoBehaviour
{
    public static EnemySpawned instance;
    void Awake() { instance = this; }

    void Start()
    {
        StartCoroutine(WaveStartDelay());
    }

    IEnumerator WaveStartDelay()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<EnemyFactory>().startSpawning();
    }
}
