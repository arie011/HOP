using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemyBullets : MonoBehaviour
{
    [Header("Prefab Parameters")]
    [SerializeField]
    protected GameObject enemyBulletPrefab;
    [SerializeField]
    protected int poolSize = 10;

    protected Queue<GameObject> enemyBulletList;

    public Transform spawnedParent;

    public void Initialize(GameObject enemyBulletPrefab, int poolSize)
    {
        this.enemyBulletPrefab = enemyBulletPrefab;
        this.poolSize = poolSize;
    }

    public GameObject SpawnObject()
    {
        SpawnObjectParentIfNeeded();

        GameObject spawnedObject;

        if(enemyBulletList.Count < poolSize)
        {
            spawnedObject = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + enemyBulletPrefab.name + "_" + enemyBulletList.Count;
            spawnedObject.transform.SetParent(spawnedParent);
        }
        else
        {
            spawnedObject = enemyBulletList.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        enemyBulletList.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void SpawnObjectParentIfNeeded()
    {
        if (spawnedParent == null)
        {
            string name = "enemyBulletList_" + enemyBulletPrefab.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
            {
                spawnedParent = parentObject.transform;
            }
            else
            {
                spawnedParent = new GameObject(name).transform;
            }
        }
    }
}