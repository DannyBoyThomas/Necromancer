using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    TargetSystem targetSystem;
    List<GameObject> targets;
    BloodPool pool;
    Entity entity;
    void Start()
    {
        targetSystem = GetComponent<TargetSystem>();
        pool = GetComponent<BloodPool>();
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.E))
        {
            GetTargets(3);
            if (Input.GetMouseButtonDown(1))
            {
                ExtractEssence();
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            GetTargets(1);
            if (Input.GetMouseButtonDown(1))
            {
                //GiveEssence();
                //GetComponent<Entity>().DealDamage(targets[0].GetComponent<EntityNPC>(), 30, DamageType.Blunt);
                entity.DealDamageTemporal(targets[0].GetComponent<EntityNPC>(), 50, 5, 0.3f, DamageType.Acid);
            }
        }
    }
    void GetTargets(int amount)
    {
        targets = targetSystem.GetTargets<EntityNPC>(amount);
    }
    
    void ExtractEssence()
    {
       
        foreach(GameObject g in targets)
        {
            EntityNPC entity  = g.GetComponent<EntityNPC>();
            if (entity != null)
            {
                pool.AbsorbPoints(entity.ExtractEssence(5));
            }
        }
    }
    void GiveEssence()
    {
        foreach (GameObject g in targets)
        {
            EntityNPC entity = g.GetComponent<EntityNPC>();
            if (entity != null)
            {
                entity.GiveEssence(pool.UsePoints(5));
            }
        }
    }
    void OnDrawGizmos()
    {
        if (targets != null)
        {
            foreach (GameObject target in targets)
            {
                Gizmos.DrawLine(transform.position, target.transform.position);
            }
        }
    }
}
