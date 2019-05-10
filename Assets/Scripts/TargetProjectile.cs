using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProjectile : MonoBehaviour
{
    public AnimationCurve yOffset;
    public AnimationCurve lerpSmoothing;

    public Transform target;
    public Vector3 origin;
    public float Speed = 5;
    public float Lerp = 0f;
    bool delayed = false;
    public bool Direction = false;
    public AudioSource CollisionSoundEffect;
    public AudioSource TravelSoundEffect;


    public Action<GameObject> OnTargetHit;

    void Start()
    {
        origin = transform.position;
        if (!Direction)
            StartCoroutine(DoTarget());
        else
            StartCoroutine(DoTargetBackwards());
    }

    IEnumerator DoTarget()
    {
        while (Lerp < 1)
        {
            Debug.Log("Do Target");
            Lerp += Speed * Time.deltaTime;
            Vector3 pos = Vector3.Lerp(origin, target.position, lerpSmoothing.Evaluate(Lerp));
            pos += Vector3.up * yOffset.Evaluate(Lerp);
            transform.position = pos;
            yield return new WaitForEndOfFrame();
        }
        OnTargetHit(target.gameObject);
        CollisionSoundEffect.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        CollisionSoundEffect.Play((ulong)UnityEngine.Random.Range(0f, 0.1f));
        TravelSoundEffect.Stop();
    }

    IEnumerator DoTargetBackwards()
    {
        Lerp = 1; OnTargetHit(target.gameObject);

        CollisionSoundEffect.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        CollisionSoundEffect.Play((ulong)UnityEngine.Random.Range(0f, 0.1f));


        while (Lerp > 0)
        {
            Debug.Log("Do Target");
            Lerp -= Speed * Time.deltaTime;
            Vector3 pos = Vector3.Lerp(origin, target.position, lerpSmoothing.Evaluate(Lerp));
            pos += Vector3.up * yOffset.Evaluate(Lerp);
            transform.position = pos;
            yield return new WaitForEndOfFrame();
        }
        TravelSoundEffect.Stop();
    }

    void OnCollisionEnter(Collision coll)
    {
        Destroy(this.gameObject);
    }

    
}
