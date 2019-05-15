using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
   

    [EnumFlagsAttribute]
    public DamageType immunity;
    [EnumFlagsAttribute]
    public DamageType resistance;
    [EnumFlagsAttribute]
    public DamageType weakness;
    // Value used to adjust damage from; weakness,resistance
    public float damageModifier = .5f;

    public virtual void Start()
    {
        
    }
   public virtual void Update()
    {
       
    }

   
    public virtual bool OnTakeDamage(Entity attacker, int initialDamage, int damage, DamageType dType)
    {
       
        return true; //if accepting damage
    }

}
