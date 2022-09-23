using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [Header("Attack Parameters")]
    public float speed = 10f;
    public float cooldown;
    public int damage;

    void Update()
    {
        transform.Translate(-transform.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealthSystem>().TakeDamage(damage);
            this.gameObject.SetActive(false);
        }
    }
}
