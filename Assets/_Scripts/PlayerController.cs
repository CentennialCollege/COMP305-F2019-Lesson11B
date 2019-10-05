using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : MonoBehaviour
{
    public HeroAnimState heroAnimState;

    public Animator heroAnimator;

    public SpriteRenderer heroSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        heroAnimState = HeroAnimState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        // Idle State
        if (Input.GetAxis("Horizontal") == 0)
        {
            heroAnimState = HeroAnimState.IDLE;
            heroAnimator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
        }


        // Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            heroAnimState = HeroAnimState.WALK;
            heroAnimator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroSpriteRenderer.flipX = false;
        }

        // Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            heroAnimState = HeroAnimState.WALK;
            heroAnimator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroSpriteRenderer.flipX = true;
        }

        // Jump
        if(Input.GetAxis("Jump") > 0)
        {
            heroAnimState = HeroAnimState.JUMP;
            heroAnimator.SetInteger("AnimState", (int)HeroAnimState.JUMP);
        }

    }
}
