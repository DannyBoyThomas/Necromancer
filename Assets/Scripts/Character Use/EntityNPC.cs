using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityNPC : Entity
{
    public int maxEssence = 10;
    public int startEssence = 10;
    private int essence = 10;
    public override void Start()
    {
        Essence = startEssence;
        base.Start();
    }

    public override void Update()
    {
        ShowColor();
        base.Update();
    }
    public int Essence
    {
        get { return essence; }
        set { essence = value; }
    }
    [ContextMenu("Extract")]
    public void ex()
    {
        ExtractEssence(5);
    }
    [ContextMenu("Give")]
    public void give()
    {
        GiveEssence(5);
    }
    public int ExtractEssence(int amount)
    {
        int temp = amount > Essence ? Essence : amount;
        Essence = Mathf.Clamp(Essence - amount, 0, maxEssence);
        return temp;
    }

    public int GiveEssence(int amount)
    {
        int space = maxEssence - Essence;
        Essence = Mathf.Clamp(Essence + amount, 0, maxEssence);

        return space > amount ? amount : space;

    }
    void ShowColor()
    {
        float p = Essence / (float)maxEssence;
        Color c = Color.Lerp(Color.grey, Color.red, p);
        GetComponent<Renderer>().material.color = c;
    }
    public override bool OnTakeDamage(Entity attacker, int initialDamage, int damage, DamageType dType)
    {
        essence -= damage;
        return true; //if accepting damage
    }
}
