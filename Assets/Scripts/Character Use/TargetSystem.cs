using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetSystem : MonoBehaviour
{
    public float radius = 10;
    List<TargetInfo> listInfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<GameObject> GetTargets<T>(int amount) where T: Component
    {
       listInfo = new List<TargetInfo>();
        float maxAngle =30;
        float dotReq = Mathf.Cos((maxAngle * Mathf.PI) / 180);
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        for(int i=0; i<cols.Length; i++)
        {
            GameObject g = cols[i].gameObject;
            if(g.GetComponent<T>())
            {
                Vector3 dir = (g.transform.position-transform.position).normalized;
                float dot = Vector3.Dot(dir, transform.forward);
                //Debug.Log(dot);
                if(dot>dotReq) //within max Angle
                {
                    TargetInfo targetInfo = new TargetInfo();
                    targetInfo.g = g;
                    targetInfo.dot = dot;
                    targetInfo.distance = Vector3.Distance(g.transform.position, transform.position);
                    listInfo.Add(targetInfo);
                }
            }
            
        }
        return ClosestTargets(listInfo, amount);
    }
    List<GameObject> ClosestTargets(List<TargetInfo> listInfo, int amount)
    {
        List<GameObject> list = new List<GameObject>();
        listInfo.Sort((s1, s2) => s1.distance.CompareTo(s2.distance));
        int min = Mathf.Min(amount, listInfo.Count);
        for(int i=0; i<min; i++ )
        {
            list.Add(listInfo[i].g);
        }
        return list;
    }
    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
        Vector3 dirA = Quaternion.AngleAxis(30, transform.up) * transform.forward;
        Vector3 dirB = Quaternion.AngleAxis(-30, transform.up) * transform.forward;
        Gizmos.DrawLine(transform.position, dirA*radius + transform.position);
        Gizmos.DrawLine(transform.position, dirB * radius + transform.position);
       

    }
    struct TargetInfo
    {
        public GameObject g;
        public float dot;
        public float distance;
    }
}
