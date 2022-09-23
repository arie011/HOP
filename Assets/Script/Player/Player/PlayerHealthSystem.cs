using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public class PlayerHealthSystem : MonoBehaviour
{
    public GameObject healthbarPrefab;
    public int maxHealth = 100, health;
    public Slider healthSlider;
    CharacterController playerMovement;
    private UnityArmatureComponent animator;

    void Start()
    {
        // Set Health, text bar dan slider
        health = maxHealth;
        healthSlider.value = health;

        animator = GetComponent<UnityArmatureComponent>();
        playerMovement = GetComponent<CharacterController>();
        
    }

    void OnPlayerDead(bool dead = false)
    {
        if (dead)
        {
            animator.animation.Play(("PlayerDead"), 1);
            StartCoroutine(PlayAnimation());
            GameManager.Instance.PlayerCondition(false);
        }
    }

    public void TakeDamage(int amount)
    {
        if (playerMovement.canMove)
        {
            health -= amount;
            healthSlider.value = health;

            if (health <= 0)
            {
                OnPlayerDead(true);
                playerMovement.canMove = false;
            }
            else
            {
                StartCoroutine(PlayerGetHit());
            }
        }


    }
    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    IEnumerator PlayerGetHit()
    {
        animator.animation.Play("PlayerGethit", 1);

        var horizontal = playerMovement.joystick.Horizontal;
        var vertical = playerMovement.joystick.Vertical;

        yield return new WaitForSeconds(0.2f);
        if (horizontal == 0 && vertical == 0)
        {
            animator.animation.Play("PlayerIdle");
        }
        else
        {
            animator.animation.Play("PlayerWalking_alternative2");
        }
    }
}
