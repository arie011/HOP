using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    public float speed = 3;
    public VariableJoystick joystick;
    PressJoystick pressJoystick;

    private UnityArmatureComponent animator;

    // Try Grid Movement
    public UnityEngine.Transform movePoint;
    public UnityEngine.Transform[] limitArea;
    public bool canMove = true;
    private bool isIdle = true;


    void Start()
    {
        animator = GetComponent<UnityArmatureComponent>();

        pressJoystick = FindObjectOfType<PressJoystick>();
        movePoint.parent = null;
        canMove = true;
        animator.animation.Play(("PlayerIdle"));
    }

    void Update()
    {
        GridMovement();
    }

    void GridMovement()
    {
        if (canMove)
        {
            var horizontal = joystick.Horizontal;
            var vertical = joystick.Vertical;

            gameObject.transform.position += new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;
            
            if(vertical == 0 && horizontal == 0)
            {
                if (isIdle == false)
                {
                    isIdle = true;
                    PlayingAnimation("PlayerIdle");
                }
            }
            else
            {
                if (isIdle == true)
                {
                    isIdle = false;
                    PlayingAnimation("PlayerWalking_alternative2");
                }
            }
        
        }
    }

    void PlayingAnimation(string animationName)
    {
        //animator.animation.Play((animationName));
        animator.animation.FadeIn(animationName, 0.25f, 0);
    }



    // Tambahan 

    public static int WeaponDMG = 100;

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("BuffDarah"))
        {
            PlayerHealthSystem phs = FindObjectOfType<PlayerHealthSystem>();
            phs.health += SistemBuff.instance.PenambahanBuff[0];
            if(phs.health >= 100)
            {
                phs.health = 100;
            }
            phs.healthSlider.value = phs.health;
            
            Destroy(trig.gameObject);
            SistemBuff.instance.SpawnBuffGui(0);
        }

        if (trig.gameObject.CompareTag("BuffPower"))
        {
            WeaponDMG = 2000;
            Destroy(trig.gameObject);
            SistemBuff.instance.SpawnBuffGui(1);
            StartCoroutine(ResetBuffPower(SistemBuff.instance.PenambahanBuff[1]));
        }
    }


    IEnumerator ResetBuffPower(int Lama)
    {
        yield return new WaitForSeconds(Lama);
        WeaponDMG = 100;
    }
}
