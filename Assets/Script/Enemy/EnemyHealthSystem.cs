using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EnemyHealthSystem : MonoBehaviour
{
    public float MaxHealth;
    public float Health = 50f;
    public int scoreValue = 100;
    public bool OnEnemyDead = false;

    public EnemyMovement enemyMovement;

    public UnityArmatureComponent anim;

    void OnEnable()
    {
        Health = MaxHealth;
        OnEnemyDead = false;
        
    }

    void Start()
    {
        Health = MaxHealth;
        OnEnemyDead = false;
    }

    public void ReduceHealth(int damage)
    {
        Debug.Log("health -");
        Health -= damage;
        if (Health > 0)
        {
            StartCoroutine(Hit());
        }
        else
        {
            if (!OnEnemyDead)
            {
                OnDead();
                PlayingAnim("enemy_dead");
                OnEnemyDead = true;
                GameManager.Instance.scoreManager.score += scoreValue;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            OnDead();
            PlayingAnim("enemy_dead");
            OnEnemyDead = true;
        }
    }

    IEnumerator Hit()
    {
        PlayingAnim("enemy_getHit");
        yield return new WaitForSeconds(0.5f);
        PlayingAnim("enemy_walk");
    }

    void OnDead()
    {       
        enemyMovement.OnDead(1.25f);
    }

    void PlayingAnim(string animName)
    {
        anim.animation.Play(animName);
    }
}
