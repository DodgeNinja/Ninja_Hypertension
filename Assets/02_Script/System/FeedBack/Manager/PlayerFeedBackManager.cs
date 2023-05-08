using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeedBackManager : FeedBackManager
{

    private PlayerInvincibility invincibility;

    private void Awake()
    {
        
        invincibility = FindObjectOfType<PlayerInvincibility>();

    }

    public override void PlayFeedback(string state)
    {

        if (invincibility.isInvincibility) return;
        base.PlayFeedback(state);

    }

}
