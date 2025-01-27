using Class;
using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCaster : MonoBehaviour
{

    [SerializeField] private List<PlayerDamageCasterClass> castingList = new List<PlayerDamageCasterClass>();

    private PlayerHP playerHP;
    private PlayerSkill skill;

    private void Awake()
    {
        
        playerHP = GetComponent<PlayerHP>();
        skill = GetComponent<PlayerSkill>();

    }

    public void CastingDamage(HPObject hpObj, string key)
    {

        var itemObj = castingList.Find(x => x.eventKey == key);

        if (itemObj == null) return;

        float damageValue = itemObj.GetDamageValue(playerHP.GetHPLV);
        hpObj.TakeDamage(damageValue);

        var obj = FAED.Pop("DamageText", hpObj.transform.position +
            new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);

        var damage = obj.GetComponent<DamageText>();
        damage.SetText(damageValue);

        playerHP.HealingHP(damageValue / 30);

    }

    public void CastingHoldAttackDamage(HPObject hpObj, string key)
    {

        var itemObj = castingList.Find(x => x.eventKey == key);

        if (itemObj == null) return;

        float damageValue = itemObj.GetDamageValue(playerHP.GetHPLV) * skill.CurrentLV;
        hpObj.TakeDamage(damageValue);

        var obj = FAED.Pop("DamageText", hpObj.transform.position +
            new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);

        var damage = obj.GetComponent<DamageText>();
        damage.SetText(damageValue);

        playerHP.HealingHP(damageValue / 30);

    }

}
