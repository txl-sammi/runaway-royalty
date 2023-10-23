using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameInterfaces {
public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField] private PlayerScript player;
    private const string IS_WALKING = "IsWalk";
    private const string IS_ATTACKING = "IsAttack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //TODO: animation looks funny while attack and walk at the same time
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_ATTACKING, player.IsAttacking());
    }
}
}