using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float speed = 500, atkSpeed = 0.8f;
    public static int damage = 100;
    private Rigidbody2D rb;    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = Vector2.right * speed;
        StartCoroutine(SetOff());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealthSystem>().ReduceHealth(CharacterController.WeaponDMG);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator SetOff()
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }
}
