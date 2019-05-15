using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalDamage : MonoBehaviour
{
    Entity attacker, entity;
    DamageType damageType;
    int totalDamage, intervalDamage;
    float intervalTime;
    float timePassed = 0;
    int damageDone = 0;
    DamageSystem.OnTemporalDamageComplete callback;
    bool active = false;

    void Start()
    {
        entity = GetComponent<Entity>();
    }
    public void Setup(Entity _attacker,int _totalDamage,int _intervalDamage, float _intervalTime, DamageType dType, DamageSystem.OnTemporalDamageComplete _callback = null)
    {
        attacker = _attacker;
        totalDamage = _totalDamage;
        intervalDamage = _intervalDamage;
        intervalTime = _intervalTime;
        damageType = dType;
        active= true;
        callback = _callback;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= intervalTime)
            {
                timePassed -= intervalTime;
                int remainingDamage = totalDamage - damageDone;
                int damageToDeal = remainingDamage > intervalDamage ? intervalDamage : remainingDamage;
                attacker.DealDamage(entity, damageToDeal, damageType);
                damageDone += damageToDeal;
                if (damageDone >= totalDamage)
                {
                    if(callback != null)
                    {
                        callback(entity);
                    }
                    Destroy(this);
                }

            }
        }
    }
}
