using Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSencer : MonoBehaviour
{

    [SerializeField] private LayerMask hitAbleLayer;
    [SerializeField] private List<HitRangeStruct> hitRanges = new List<HitRangeStruct>();

    public bool swap { get; set; }

    public void ChackHit(string hitName)
    {

        var hit = hitRanges.Find(x => x.atkKey == hitName);

        var hitArr = Physics2D.BoxCastAll(
        swap ? 
        transform.position + new Vector3(-hit.offSet.x, hit.offSet.y) : 
        transform.position - new Vector3(hit.offSet.x, hit.offSet.y),
        hit.range, 0, Vector2.zero, 0, hitAbleLayer);
        if(hitArr.Length != 0)
        {
            foreach(var item in hitArr)
            {
                item.transform.GetComponent<FeedBackManager>()?.PlayFeedback("Hit");
            }
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        foreach(var item in hitRanges)         
        {
            Gizmos.color = item.gizmoColor;
            Gizmos.DrawWireCube(swap ? transform.position + new Vector3(-item.offSet.x, item.offSet.y) : transform.position - new Vector3(item.offSet.x, item.offSet.y), item.range);;
            Gizmos.color = Color.white;

        }

    }

#endif

}