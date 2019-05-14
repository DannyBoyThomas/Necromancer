using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxEssence = 10;
    public int startEssence = 10;
    private int essence = 10;
   
    void Start()
    {
        Essence = startEssence;
    }
    void Update()
    {
        ShowColor();
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
        Essence = Mathf.Clamp(Essence-amount,0, maxEssence);
        return temp;
    }
  
    public int GiveEssence(int amount)
    {
        int space = maxEssence - Essence;
        Essence = Mathf.Clamp(Essence + amount,0, maxEssence);

        return space > amount ? amount : space;

    }
    void ShowColor()
    {
        float p = Essence / (float)maxEssence ;
        Color c = Color.Lerp(Color.grey, Color.red, p);
        GetComponent<Renderer>().material.color = c;
    }

}
