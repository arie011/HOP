using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class EnemyMovement : MonoBehaviour
{
    //Inisiasi variabel yang dibutuhkan
    public float speed = 1f;

    public UnityArmatureComponent anim;

    public UnityEngine.Transform pullObject;
    private float delay;
    //bool moving = true;

    void Start()
    {
        PlayingAnim("enemy_walk");
    }

    //private void OnEnable()
    //{
    //    PlayingAnim("enemy_walk");
    //}

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!GameManager.Instance.isGameOver)
        {
            transform.Translate(-transform.right * speed * Time.deltaTime);
        }
    }

    public IEnumerator TurnOffEnemy(UnityEngine.Transform pullObject, float delay)
    {
        this.pullObject = pullObject;
        this.delay = delay;
        anim.animation.Play("enemy_idle");

        yield return new WaitForSeconds(delay);
        gameObject.transform.SetParent(pullObject);
        gameObject.SetActive(false);

    }

    public void OnDead(float delay)
    {
        StopAllCoroutines();
        StartCoroutine(TurnOffEnemy(pullObject, delay));
    }

    void PlayingAnim(string animName)
    {
        anim.animation.FadeIn(animName, 0.2f, 0);
    }
}
