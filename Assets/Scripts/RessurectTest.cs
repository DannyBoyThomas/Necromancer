using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessurectTest : MonoBehaviour
{
    public Transform[] Targets;
    public TargetProjectile projectile;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            foreach(Transform t in Targets)
            {
                var g = GameObject.Instantiate(projectile);
                g.transform.position = transform.position;
                g.GetComponent<TargetProjectile>().target = t;
                g.GetComponent<TargetProjectile>().OnTargetHit += OnTargetHit;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Transform t in Targets)
            {
                var g = GameObject.Instantiate(projectile);
                g.transform.position = transform.position;
                g.GetComponent<TargetProjectile>().target = t;
                g.GetComponent<TargetProjectile>().OnTargetHit += OnTargetHit2;
                g.GetComponent<TargetProjectile>().Direction = true;
            }
        }
    }

    void OnTargetHit(GameObject g)
    {
        g.GetComponent<Animator>().SetTrigger("Ressurect");
    }
    
      void OnTargetHit2(GameObject g)
    {
        g.GetComponent<Animator>().SetTrigger("Extract");
    }
}
