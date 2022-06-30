using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        switch (player.state)
        {
            case PlayerStates.RUN:
                animator.SetTrigger("Run");
            break;

            case PlayerStates.JUMP:
                animator.SetTrigger("Jump");
            break;

            case PlayerStates.FALL:
                animator.SetTrigger("Fall");
            break;

            case PlayerStates.FLY:
                animator.SetTrigger("Fly");
            break;

            default:
                Debug.Log("Error state");
            break;
        }
    }
}
