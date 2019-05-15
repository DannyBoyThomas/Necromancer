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
    public bool stoodInAcidPool = false;
    public virtual void Start()
    {
        
    }
   public virtual void Update()
    {
       
    }

   
    public virtual int OnTakeDamage(Entity attacker, int initialDamage, int damage, DamageType dType)
    {
       
        return damage; //if accepting damage
    }
    public  void DieAsync()
    {
        StartCoroutine(Die());
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
