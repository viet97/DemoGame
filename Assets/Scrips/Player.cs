using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : BasePlayer
{
    private State state;
    private Vector3 targetPosition;
    private Vector3 startingPosition;
    private Player targetPlayer;
    private Action onSlideComplete;
    private Action onAttackComplete;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] Transform spriteRendererTransform;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    private String attackTypeBoolString;
    private int dame = 20;
    private bool isDead = false;
    private int health = 100;
    private enum State
    {
        Idle,
        Sliding,
        Attacking,
        Busy,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        startingPosition = transform.position;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
    public Animator GetAnimator()
    {
        return animator;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Sliding) {
            float reachedDistance = 10f;
            if (Vector3.Distance(transform.position, targetPosition) < reachedDistance)
            {
                //stand and attack
                animator.SetFloat("Speed", 0);
                onSlideComplete();
                onSlideComplete = null;
                return;
            }
            float slideSpeed = 5f;
            transform.position += (targetPosition - startingPosition) * slideSpeed * 0.003f;
        }
    }

    public void Attack(Player targetPlayer, Vector3 targetPosition,String attackTypeBoolString, Action onAttackComplete)
    {
        this.attackTypeBoolString = attackTypeBoolString;
        this.onAttackComplete = onAttackComplete;
        this.targetPlayer = targetPlayer;
        Debug.Log("Attack" + targetPosition.x +" " + startingPosition);
        slideToPosition(targetPosition, () =>
        {
            //do attack
            if (!animator.GetBool(this.attackTypeBoolString))
            {
                animator.SetBool(this.attackTypeBoolString, true);
            }
        });
    }
    private void Attacking()
    {
        //deal dame to target
        targetPlayer.Damage(dame);
        //make target player hurt
        targetPlayer.GetAnimator().SetBool("isHurt", true);
    }
    private void AttackFinish()
    {
        Debug.Log("AttackFinish");
        animator.SetBool(this.attackTypeBoolString, false);
        //move back after attack finish
        Vector3 newTargetPosition = startingPosition;
        spriteRenderer.flipX = true;
        slideToPosition(newTargetPosition, () =>
        {
            spriteRenderer.flipX = false;
            state = State.Idle;
            //attack complete
            this.onAttackComplete();
        });
    }
    private void HurtFinish()
    {
        Debug.Log("Hurt finish");
        GetAnimator().SetBool("isHurt", false);
    }
    public void Damage(int dame)
    {
        health -= dame;
        if (health <= 0) isDead = true;
        healthBar.setHealth(health);
    }

    void slideToPosition(Vector3 targetPosition,Action onSlideComplete)
    {
        startingPosition = transform.position;
        this.targetPosition = targetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
        animator.SetFloat("Speed", 1);
    }
}
