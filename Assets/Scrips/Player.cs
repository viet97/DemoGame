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

    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Sliding) {
            float slideSpeed = 5f;
            transform.position += (targetPosition - transform.position) * slideSpeed * Time.deltaTime;

            float reachedDistance = 1f;
            if (Vector3.Distance(transform.position, targetPosition) < reachedDistance)
            {
                onSlideComplete();
            }
        }
    }

    public void Attack(Vector3 targetPosition, Action<int> onAttackComplete)
    {
        slideToPosition(targetPosition, () =>
        {
            //do attack animation and deal dame
            slideToPosition(startingPosition, () =>
            {
                state = State.Idle;
                onAttackComplete(dame);
            });
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

    }
}
