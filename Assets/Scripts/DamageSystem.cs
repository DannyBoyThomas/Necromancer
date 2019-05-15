using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum DamageType { Blunt, Fire, Acid };

public static class DamageSystem 
{
    static GameObject prefab = null;
    public delegate void OnTemporalDamageComplete(Entity entity);
    public static bool DealDamage(this Entity attacker, Entity entity, int val, DamageType dType = DamageType.Blunt)
    { 
        float modifier = entity.damageModifier;
        //val = dType == immunity ? 0 : dType == resistance ? (int)(val / modifier) : dType==weakness?(int)(val*modifier):val;
        int weaknessMod = EnumArrayContains(dType, entity.weakness) ? (int)(val*modifier):0;
        int resistanceMod = EnumArrayContains(dType, entity.resistance) ? (int)(-val *modifier) : 0;
        int tempDamage = val + weaknessMod + resistanceMod;
        int damage = EnumArrayContains(dType, entity.immunity) ? 0 : tempDamage;
        //Debug.Log("weakness: " + weaknessMod + ", resistance: " + resistanceMod);
        //Debug.Log(val);
       int didDamage = entity.OnTakeDamage(attacker, val, damage, dType);
        if(didDamage>0)
        {
            DisplayDamageInstance(entity, didDamage, dType);
        }
        return didDamage>0;
    }
    public static void DealDamageMultiple(this Entity attacker,List<Entity> entities, int val, DamageType dType)
    {
        for(int i=0; i<entities.Count; i++)
        {
            attacker.DealDamage(entities[i], val, dType);
        }
    }
    //Deal incremental damage over time
    public static void DealDamageTemporal(this Entity attacker, Entity entity, int damageTotal, int damageInterval, float timeInterval, DamageType dType, OnTemporalDamageComplete callback = null )
    {
        TemporalDamage td = entity.gameObject.AddComponent<TemporalDamage>();
        td.Setup(attacker, damageTotal, damageInterval, timeInterval, dType, callback);
    }
    //Check if a damage type exists within a List<DamageType>
    //Its an enum so is actually in bits rather than an actual list
    static bool EnumArrayContains(DamageType search, DamageType container)
    {
        List<DamageType> list = GetSelectedElements(container);
        return list.Contains(search);
    }
    static List<DamageType> GetSelectedElements(DamageType container)
    {
        List<DamageType> selectedElements = new List<DamageType>();
        for (int i = 0; i < System.Enum.GetValues(typeof(DamageType)).Length; i++)
        {
            int layer = 1 << i;
            if (((int)container & layer) != 0)
            {
                selectedElements.Add((DamageType)i);
            }
        }
        return selectedElements;

    }
    public static Color GetColor(this DamageType dType)
    {
        switch(dType)
        {
            case DamageType.Acid:return Color.green;
            case DamageType.Blunt: return Color.black;
            case DamageType.Fire: return Color.red;
        }
        return Color.white;
    }
    static void DisplayDamageInstance(Entity entity, int damage, DamageType dType)
    {
        LoadPrefab();
        Color color = dType.GetColor();
        GameObject text = GameObject.Instantiate(prefab, entity.transform.position, Quaternion.identity);
        text.transform.forward = Camera.main.transform.forward;
        TMPro.TMP_Text tm = text.GetComponentInChildren<TMPro.TMP_Text>();
        tm.text = damage.ToString();
        tm.color = color;
        
    }
    static void LoadPrefab()
    {
        if(prefab == null)
        {
            prefab = (GameObject)Resources.Load("Prefabs/Popup Text Parent");
        }
       
    }
    
}
