using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    TargetSystem targetSystem;
    List<GameObject> targets;
    BloodPool pool;
    void Start()
    {
        targetSystem = GetComponent<TargetSystem>();
        pool = GetComponent<BloodPool>();
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
                GiveEssence();
            }
        }
    }
    void GetTargets(int amount)
    {
        targets = targetSystem.GetTargets(amount);
    }
    
    void ExtractEssence()
    {
       
        foreach(GameObject g in targets)
        {
            Entity entity  = g.GetComponent<Entity>();
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
            Entity entity = g.GetComponent<Entity>();
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
