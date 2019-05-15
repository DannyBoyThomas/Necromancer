using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider collider)
    {
        Debug.Log("entered");
        GameObject g = collider.gameObject;
        if (g.GetComponent<Entity>())
        {
            Entity entity = g.GetComponent<Entity>();
            if (!entity.stoodInAcidPool)
            {
                entity.DealDamageTemporal(entity, 20, 5, 1, DamageType.Acid, RemoveAcidEffect);
                entity.stoodInAcidPool = true;
            }
        }
    }
    void RemoveAcidEffect(Entity entity)
    {
        entity.stoodInAcidPool = false;
    }
}
