using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMyCube : MonoBehaviour
{
    public float Speed;
    Rigidbody m_rigidbody;
    private void Start()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            MoveCube(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.C))
        {
            MoveCube(Vector3.forward);
        }
    }
    private void MoveCube(Vector3 dir)
    {
        m_rigidbody.MovePosition(this.transform.position + dir * Speed * Time.deltaTime);
    }

}
