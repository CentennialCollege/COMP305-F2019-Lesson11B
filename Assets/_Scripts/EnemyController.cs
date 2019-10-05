using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class EnemyController : MonoBehaviour
{
    public EnemyAnimState enemyAnimState;

    public Animator enemyAnimator;

    public SpriteRenderer enemySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimState = EnemyAnimState.WALK;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
