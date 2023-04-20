using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private readonly int MoveVelXHash = Animator.StringToHash("MoveVelX");
    private readonly int MoveVelYHash = Animator.StringToHash("MoveVelY");
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private readonly int LandingTriggerHash = Animator.StringToHash("LandingTrigger");
    private readonly int WallFallHash = Animator.StringToHash("WallFall");
    private readonly int AttackTriggerHash = Animator.StringToHash("AttackTrigger");
    private readonly int ComboCountHash = Animator.StringToHash("ComboCount");
    private readonly int AttackEndHash = Animator.StringToHash("AttackEnd");
    private readonly int IsAirHash = Animator.StringToHash("IsAir");

    private MargedSencer margedSencer;
    private PlayerFlip playerFlip;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigid;
    private JumpCol jumpCol;
    private float fallDownTime;

    private void Awake()
    {
        
        rigid = GetComponent<Rigidbody2D>();
        playerFlip = GetComponent<PlayerFlip>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        jumpCol = GetComponentInChildren<JumpCol>();
        margedSencer = GetComponentInChildren<MargedSencer>();

    }

    private void Update()
    {

        SetMoveVelHash();
        FallDownChack();
        ChackLanding();
        WallChack();
        SetIsAir();

    }

    private void SetMoveVelHash()
    {

        animator.SetFloat(MoveVelXHash, Mathf.Abs(rigid.velocity.x));
        animator.SetFloat(MoveVelYHash, rigid.velocity.y);

    }

    private void FallDownChack()
    {

        if(rigid.velocity.y < 0) 
        {
            
            fallDownTime += Time.deltaTime;

        }
        else
        {

            fallDownTime = 0;

        }


    }

    private void ChackLanding()
    {

        if(fallDownTime > 1f && jumpCol.isGround == true)
        {

            fallDownTime = 0;
            animator.SetTrigger(LandingTriggerHash);

        }

    }

    private void WallChack()
    {

        if((margedSencer.RightSencer || margedSencer.LeftSencer) && fallDownTime > 0f)
        {

            animator.SetFloat(WallFallHash, 1);

            if(margedSencer.RightSencer) spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;

            playerFlip.useFlip = false;


        }
        else
        {

            animator.SetFloat(WallFallHash, 0);
            playerFlip.useFlip = true;

        }

    }

    private void SetIsAir()
    {

        animator.SetFloat(IsAirHash, rigid.velocity.y == 0 ? 0 : 1);

    }

    public void SetJump()
    {

        animator.SetTrigger(JumpHash);

    }

    public void SetAttackTrigger()
    {

        animator.ResetTrigger(AttackEndHash);
        animator.SetTrigger(AttackTriggerHash);

    }

    public void SetComboCount(int count)
    {

        animator.SetInteger(ComboCountHash, count);

    }

    public void ResetComboCount()
    {

        animator.SetInteger(ComboCountHash, 0);

    }

    public void SetEndAttack()
    {

        animator.SetTrigger(AttackEndHash);

    }

}