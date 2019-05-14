using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    new public Rigidbody rigidbody;
    public float movementSpeed;
    public float rotationSpeed = 10;

    private Animator _animator;
    public bool CanMove = true;

    void Start()
    {
        _animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float yRot = Input.GetAxis("Mouse X");


        Vector3 moveDirection = transform.TransformDirection(new Vector3(h, 0, v));
        moveDirection.y = 0;

        Debug.DrawRay(transform.position, moveDirection * 5f, Color.yellow);
        if (CanMove)
        {
            rigidbody.MovePosition(transform.position + moveDirection * Time.deltaTime * movementSpeed);

            transform.Rotate(Vector3.up * yRot * rotationSpeed * Time.deltaTime, Space.World);

            Debug.DrawRay(transform.position, moveDirection * 5f, Color.yellow);
        }

        AnimateMovement(h, v);
    }

    public void AnimateMovement(float h, float v)
    {
        _animator.SetFloat("Horizontal", h);
        _animator.SetFloat("Vertical", v);
    }
}
