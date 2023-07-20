using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveState : AIState
{
    [SerializeField] private float moveSpeed;

    private Transform target;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    protected override void Awake()
    {

        base.Awake();
        rigid = controller.GetComponent<Rigidbody2D>();
        spriteRenderer = controller.GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform;

    }

    public override void EnterState()
    {



    }

    public override void ExitState()
    {

        rigid.velocity = Vector2.zero;

    }

    public override void UpdateState()
    {

        float crtSpeed = (transform.position.x - target.position.x) > 0 ? -moveSpeed : moveSpeed;

        rigid.velocity = new Vector2(crtSpeed, 0);
        spriteRenderer.flipX = (transform.position.x - target.position.x) < 0;

    }
}
