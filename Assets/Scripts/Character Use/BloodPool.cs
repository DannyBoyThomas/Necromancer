using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPool : MonoBehaviour
{
    private int points = 0;
    public int startPoints =10;
    public int maxPoints = 100;

    void Start()
    {
        Points = startPoints;
    }

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    public int UsePoints(int val)
    {

        int temp = val > points ? points : val;
        points -= temp;
        return temp;
    }

    public void AbsorbPoints(int val)
    {
        points = Mathf.Clamp(points + val, 0, maxPoints);
    }


}
