using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagers : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int spawnCount;

    public List<GameObject> bulletList;
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;
            bulletList.Add(bullet);
            bullet.transform.parent = this.transform;
            bullet.SetActive(false);
        }
        
    }
}
