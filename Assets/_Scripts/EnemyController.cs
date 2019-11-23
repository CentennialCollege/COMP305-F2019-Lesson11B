using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class EnemyController : MonoBehaviour
{
    public EnemyAnimState enemyAnimState;
    public Animator enemyAnimator;
    public SpriteRenderer enemySpriteRenderer;
    public Rigidbody2D enemyRigidBody;
    public bool isGrounded;
    public bool hasGroundAhead;
    public bool hasWallAhead;
    public Transform lookAhead;
    public Transform wallAhead;
    public bool isFacingRight = true;
    public float movementSpeed;

    public HealthBarController healthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimState = EnemyAnimState.WALK;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            healthBar.SetDamage(0.2f);
        }

        Move();
    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position, new Vector2(2.0f, 1.0f), 0.0f, Vector2.down, 1.0f, 1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        hasWallAhead = Physics2D.Linecast(
            transform.position,
            wallAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded)
        {
            if (isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(movementSpeed, 0.0f);
            }

            if (!isFacingRight)
            {
                enemyRigidBody.velocity = new Vector2(-movementSpeed, 0.0f);
            }

            if (!hasGroundAhead || hasWallAhead)
            {
                transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
                isFacingRight = !isFacingRight;
            }
        }
    }
}
