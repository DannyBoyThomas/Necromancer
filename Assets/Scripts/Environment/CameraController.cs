using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject user;
    public float minRadius = 5.5f, maxRadius = 7f, heightOffset = 3.5f;

    public Vector3 posOffset,rotOffset;
    public float speed = 2f;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position - user.transform.position;
        rotOffset = transform.localRotation.eulerAngles;
        target =LocalVector(posOffset);
    }

    // Update is called once per frame
    void Update()
    {
        KeepView();
    }
    Vector3 LocalVector(Vector3 vec)
    {
        return user.transform.position + vec;
    }
   
    void KeepView()
    {
        //if(IsDefaultViewAvailable())
        //{
        //    target = DefaultTarget();
        //}
        //else
        if(true)
        {
            target = GetClosestVector(GetLineOfSights());
        }
        // When default target available, use it
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        transform.LookAt(LocalVector(Vector3.up * 2));
    }
    bool CanSeeUser()
    {
        Vector3 dir = user.transform.position - transform.position;
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxRadius * 2))
        {
            if(hit.collider.gameObject == user)
            {
                return true;
            }
        }
        return false;
    }
    bool IsDefaultViewAvailable()
    {
        Vector3 dir = user.transform.position - DefaultTarget();
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, maxRadius * 2))
        {
            return true;
        }
        return false;
    }
    Vector3 DefaultTarget()
    {
        Vector3 offset = user.transform.forward * -minRadius + user.transform.up*heightOffset;
        return LocalVector(offset);
    }
    
    List<Vector3> GetLineOfSights()
    {
        List<Vector3> list = new List<Vector3>();
        Vector3 dir = new Vector3(0, heightOffset, minRadius);
        float magnitude = dir.magnitude;
        int numOfRays = 36;
        for(int i=0; i<numOfRays; i++)
        {
            float angle = (360 / numOfRays) * i;
            Vector3 currentDir = Quaternion.Euler(0, angle, 0) * dir;
            Ray ray = new Ray(user.transform.position, currentDir);
            RaycastHit hit;
            if(!Physics.Raycast(ray, out hit, magnitude))
            {
                list.Add(user.transform.position + currentDir);
            }
        }
        return list;
    }
    Vector3 GetClosestVector(List<Vector3> list)
    {
        if(list != null)
        {
            Vector3? closest = null;
            float smallDistance = Mathf.Infinity;
            for(int i=0; i< list.Count; i++)
            {
                Vector3 vec = list[i];
                float distance = Vector3.Distance(DefaultTarget(), vec);
                if(distance<smallDistance)
                {
                    smallDistance = distance;
                    closest = vec;
                }
            }
            if(closest != null)
            {
                return (Vector3)closest;
            }
        }
        return DefaultTarget();
    }
    void OnDrawGizmos()
    {


       Gizmos.color = Color.blue;
    }
}
