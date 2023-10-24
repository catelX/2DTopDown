using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBtest : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = 2 * Vector3.right;
        Debug.Log(rb.velocity);
    }
}
