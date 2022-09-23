using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class PlayerShoot : MonoBehaviour
{
    public CharacterController playerMovement;
    public UnityEngine.Transform shootingPos;
    float time;
    private PoolManagers _pool;
    private UnityArmatureComponent animator;
    [SerializeField] WeaponScript weapon;

    void Start()
    {
        _pool = GameObject.FindObjectOfType<PoolManagers>();
        animator = GetComponent<UnityArmatureComponent>();
    }

    
    public void Shooting()
    {
        Vector3 pos = shootingPos.position;

        if (!GameManager.Instance.isGameOver)
        {
            for (int i = 0; i < _pool.bulletList.Count; i++)
            {
               
                if (_pool.bulletList[i].activeInHierarchy == false)
                {
                    _pool.bulletList[i].SetActive(true);
                    _pool.bulletList[i].transform.position = pos;
                    _pool.bulletList[i].transform.rotation = Quaternion.identity;
                    break;
                }
            }

            StartCoroutine(PlayShootingAnimation());
            time = 0;
        }
    }

    IEnumerator PlayShootingAnimation()
    {
        animator.animation.Play(("PlayerShooting_alternative2"), 1);

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
