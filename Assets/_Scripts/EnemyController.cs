using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class EnemyController : MonoBehaviour
{
    [Header("Animation Settings")]
    public EnemyAnimState enemyAnimState;
    public Animator enemyAnimator;

    [Header("Movement Settings")]
    public LayerMask movementLayerMask;
    public Rigidbody2D enemyRigidBody;
    public bool isGrounded;
    public bool hasGroundAhead;
    public bool hasWallAhead;
    public Transform lookAhead;
    public Transform wallAhead;
    public bool isFacingRight = true;

    [Header("HealthBar Settings")]
    public HealthBarController healthBar;
    public Transform healthBarTransform;

    [Header("Enemy Abilities")]
    public float speed = 1.5f;
    public float strength = 10.0f;
    public float toughness = 1.0f;
    public float maximumHealth = 10.0f;
    public float currentHealth = 10.0f;

    [Header("Enemy AI")]
    public LayerMask AILayerMask;
    public EnemyState enemyState;
    public Transform lineOfSight;
    public bool hasSeenPlayer;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimState = EnemyAnimState.WALK;
        enemyState = EnemyState.PATROL;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHealth -= (maximumHealth * 0.2f);
            healthBar.SetDamage(0.2f);
        }

        Move();
        HealthBarUpdate();
        CheckEnemyState();
        TakeAction();
    }

    private void CheckEnemyState()
    {
        Debug.DrawLine(transform.position, lineOfSight.position, Color.yellow);

        if (!hasSeenPlayer)
        {
            hasSeenPlayer = Physics2D.Linecast(
                transform.position,
                lineOfSight.position,
                AILayerMask);

            var hit = Physics2D.Linecast(
                transform.position,
                lineOfSight.position,
                AILayerMask);

            player = hit.transform;
        }
        

        if (hasSeenPlayer)
        {
            if (currentHealth > (0.5 * maximumHealth))
            {
                enemyState = EnemyState.PURSUE;

                if (Vector2.Distance(transform.position, player.position) < 2.0f)
                {
                    enemyState = EnemyState.ATTACK;
                }
            }

            if (Vector2.Distance(transform.position, player.position) > 10.0f)
            {
                enemyState = EnemyState.PATROL;
            }
        }

        if (currentHealth < (0.35 * maximumHealth))
        {
            enemyState = EnemyState.FLEE;
        }
    }

    private void TakeAction()
    {
        switch (enemyState)
        {
            case EnemyState.PATROL:
                speed = 1.5f;
                break;
            case EnemyState.PURSUE:
                speed = 5.0f;
            {
                // check where the player is and pursue in the proper direction
                var direction =  player.position - transform.position;

                if (Mathf.Sign(direction.x) != Mathf.Sign(transform.localScale.x))
                {
                    transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                    enemyRigidBody.velocity = new Vector2(-speed, 0.0f);
                    isFacingRight = !isFacingRight;
                }
            }
                
                break;
            case EnemyState.ATTACK:
                break;
            case EnemyState.FLEE:
                break;
        }

    }

    
    private void HealthBarUpdate()
    {
        Vector3 healthbarOffset = new Vector3(0.6f, 0.0f, 0.0f);
        healthBar.gameObject.transform.position = healthBarTransform.position - healthbarOffset;
    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            movementLayerMask);

        hasWallAhead = Physics2D.Linecast(
            transform.position,
            wallAhead.position,
            movementLayerMask);

        if (isGrounded)
        {
            if (isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(speed, 0.0f);
            }

            if (!isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(-speed, 0.0f);
            }

            if (!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                isFacingRight = !isFacingRight;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentHealth -= (maximumHealth * 0.1f);
            healthBar.SetDamage(0.1f);
        }
    }
}
