using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessurectTest : MonoBehaviour
{
    public Transform[] Targets;
    public TargetProjectile projectile;
    public Transform VFX_SpawnPoint;
    public GameObject VFX_Hand;
    public GameObject ResurrectionEffect;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Animator>().SetTrigger("Resurrect");

            // foreach (Transform t in Targets)
            // {
            //     var g = GameObject.Instantiate(projectile);
            //     g.transform.position = transform.position;
            //     g.GetComponent<TargetProjectile>().target = t;
            //     g.GetComponent<TargetProjectile>().OnTargetHit += OnTargetHit;
            // }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Transform t in Targets)
            {
                var g = GameObject.Instantiate(projectile);
                g.transform.position = t.position;
               GameObject.Instantiate(ResurrectionEffect, t.transform.position, Quaternion.identity);
                t.GetComponent<Animator>().SetTrigger("Extract");
                g.GetComponent<TargetProjectile>().target = VFX_SpawnPoint;
                g.GetComponent<TargetProjectile>().OnTargetHit += OnTargetHit2;
                g.GetComponent<TargetProjectile>().Direction = false;
            }
        }
    }

    void OnTargetHit(GameObject g)
    {
        g.GetComponent<Animator>().SetTrigger("Ressurect");
        GameObject.Instantiate(ResurrectionEffect, g.transform.position, Quaternion.identity);
    }

    void OnTargetHit2(GameObject g)
    {
        //        g.GetComponent<Animator>().SetTrigger("Extract");
          GetComponent<Animator>().SetTrigger("Extract");
    }

    void Res_Release()
    {
        foreach (Transform t in Targets)
        {
            var g = GameObject.Instantiate(projectile);
            g.transform.position = VFX_SpawnPoint.position;
            g.GetComponent<TargetProjectile>().target = t;
            g.GetComponent<TargetProjectile>().OnTargetHit += OnTargetHit;
        }
    }

    void Res_ActivateEffect()
    {
        VFX_Hand.SetActive(false);
        VFX_Hand.SetActive(true);
    }
}
