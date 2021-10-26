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
    [SerializeField] private HealthBar healthBar;
    [SerializeField] Transform spriteRendererTransform;
    [SerializeField] Animator animator;

    private SpriteRenderer spriteRenderer;
    private int dame = 20;
    private bool isDead = false;
    private int health = 100;
    private enum State
    {
        Idle,
        Sliding,
        Busy,
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        startingPosition = transform.position;
        spriteRenderer = spriteRendererTransform.GetComponent<SpriteRenderer>();
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Sliding) {
            float slideSpeed = 5f;
            transform.position += (targetPosition - startingPosition) * slideSpeed * 0.001f;

            float reachedDistance = 10f;
            if (Vector3.Distance(transform.position, targetPosition) < reachedDistance)
            {
                animator.SetFloat("Speed", 0);
                onSlideComplete();
            }
        }
    }

    public void Attack(Vector3 targetPosition, Action<int> onAttackComplete)
    {
        slideToPosition(targetPosition, () =>
        {
            animator.SetBool("isAttack", true);

            //do attack animation and deal dame
            animator.SetBool("isAttack", false);

            //slideToPosition(startingPosition, () =>
            //{
            //    state = State.Idle;
            //    onAttackComplete(dame);
            //    animator.SetFloat("Speed", 0);
            //});
        });
    }

    public void Damage(int dame)
    {
        health -= dame;
        if (health <= 0) isDead = true;
        healthBar.setHealth(health);
    }

    void slideToPosition(Vector3 targetPosition,Action onSlideComplete)
    {
        this.targetPosition = targetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
        animator.SetFloat("Speed", 1);
    }
}
