using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using UnityEngine.EventSystems;

public class PressJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool press;
    [SerializeField] UnityArmatureComponent animator;

    private void Start()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //PlayingAnimation("PlayerWalking_alternative2");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       // PlayingAnimation("PlayerIdle");
    }

    void PlayingAnimation(string animationName)
    {
        //animator.animation.Play((animationName));
        animator.animation.FadeIn(animationName, 0.25f, 0);
    }
}
