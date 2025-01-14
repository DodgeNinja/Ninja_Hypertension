using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    #region Hash    

    private readonly int MoveVelXHash = Animator.StringToHash("MoveVelX");
    private readonly int MoveVelYHash = Animator.StringToHash("MoveVelY");
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private readonly int LandingTriggerHash = Animator.StringToHash("LandingTrigger");
    private readonly int WallFallHash = Animator.StringToHash("WallFall");
    private readonly int AttackTriggerHash = Animator.StringToHash("AttackTrigger");
    private readonly int ComboCountHash = Animator.StringToHash("ComboCount");
    private readonly int AttackEndHash = Animator.StringToHash("AttackEnd");
    private readonly int IsAirHash = Animator.StringToHash("IsAir");
    private readonly int SkillHoldHash = Animator.StringToHash("SkillHold");
    private readonly int SkillHoldTriggerHash = Animator.StringToHash("SkillHoldTrigger");
    private readonly int DashTriggerHash = Animator.StringToHash("DashTrigger");
    private readonly int IsDashHash = Animator.StringToHash("IsDash");
    private readonly int DieTriggerHash = Animator.StringToHash("DieTrigger");
    private readonly int IsGroundHash = Animator.StringToHash("IsGround");
    private readonly int IsHitHash = Animator.StringToHash("IsHit");
    private readonly int JumpAttackTriggerHash = Animator.StringToHash("JumpAttackTrigger");
    private readonly int RollingTriggerHash = Animator.StringToHash("AirRollTrigger");
    private readonly int RollingEndTriggerHash = Animator.StringToHash("RollingEndTrigger");
    private readonly int PoundTriggerHash = Animator.StringToHash("PoundTrigger");
    private readonly int PoundEndTriggerHash = Animator.StringToHash("PoundEndTrigger");
    private readonly int DashAttackTriggerHash = Animator.StringToHash("DashAttackTrigger");
    private readonly int DashAttackHoldingEndTriggerHash = Animator.StringToHash("DashAttackHoldingEndTrigger");
    private readonly int IsRunHash = Animator.StringToHash("IsRun");

    #endregion

    private MargedSencer margedSencer;
    private PlayerFlip playerFlip;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigid;
    private JumpCol jumpCol;
    private float fallDownTime;

    public bool fallDownAble { get; set; } = true;

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
        ChackLanding();
        FallDownChack();
        WallChack();
        SetIsAir();
        SetIsGround();

    }
    private void SetIsGround()
    {

        animator.SetBool(IsGroundHash, jumpCol.isGround);

    }
    private void SetMoveVelHash()
    {

        animator.SetFloat(MoveVelXHash, Mathf.Abs(rigid.velocity.x));
        animator.SetFloat(MoveVelYHash, rigid.velocity.y);

    }
    private void FallDownChack()
    {

        if(rigid.velocity.y < 0 && jumpCol.isGround == false) 
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

        if(fallDownTime > 1f && jumpCol.isGround == true && fallDownAble)
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

        animator.SetFloat(IsAirHash, jumpCol.isGround ? 0 : 1);

    }
    public void SetAttackTrigger()
    {

        animator.ResetTrigger(AttackEndHash);
        animator.SetTrigger(AttackTriggerHash);

    }

    public void SetIsRun(float value) => animator.SetFloat(IsRunHash, value);
    public void SetSkillHoldHash(bool value) => animator.SetBool(SkillHoldHash, value);
    public void SetIsDash(bool value) => animator.SetBool(IsDashHash, value);
    public void SetIsHit(bool value) => animator.SetBool(IsHitHash, value); 
    public void SetComboCount(int count) => animator.SetInteger(ComboCountHash, count);
    public void ResetComboCount() => animator.SetInteger(ComboCountHash, 0);
    public void SetEndAttack() => animator.SetTrigger(AttackEndHash);
    public void SetDashAttackHoldingEndTrigger() => animator.SetTrigger(DashAttackHoldingEndTriggerHash);
    public void SetSkillHoldTriggerHash() => animator.SetTrigger(SkillHoldTriggerHash);
    public void ResetLandingTrigger() => animator.ResetTrigger(LandingTriggerHash);
    public void SetDashTrigger() => animator.SetTrigger(DashTriggerHash);
    public void SetDieTrigger() => animator.SetTrigger(DieTriggerHash);
    public void SetJumpAttackTrigger() => animator.SetTrigger(JumpAttackTriggerHash);
    public void SetRollingTrigger() => animator.SetTrigger(RollingTriggerHash);
    public void SetRollingEndTrigger() => animator.SetTrigger(RollingEndTriggerHash);
    public void SetPoundTrigger() => animator.SetTrigger(PoundTriggerHash);
    public void SetPoundEndTrigger() => animator.SetTrigger(PoundEndTriggerHash);
    public void SetDashAttackTrigger() => animator.SetTrigger(DashAttackTriggerHash);
    public void SetJump() => animator.SetTrigger(JumpHash);

}