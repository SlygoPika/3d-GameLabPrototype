using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

    public Rigidbody rb;
    public float extraRadius;
    public static List<Attractor> Attractors;

    private float G = 6.674f;

    private void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    private void OnEnable()
    {
        if (Attractors == null)
        {
            Attractors = new List<Attractor>();
        }

        Attractors.Add(this);
    }

    private void OnDisable()
    {
        Attractors.Remove(this);
    }

    void Attract(Attractor other)
    {
        Rigidbody otherRb = other.rb;

        Vector3 direction = rb.position - otherRb.position;
        float radius = direction.magnitude;

        Debug.Log(radius);

        float forceMagnitude = G * rb.mass * otherRb.mass / Mathf.Pow(radius + extraRadius, 2);
        Vector3 force = forceMagnitude * direction.normalized;

        otherRb.AddForce(force);
    }
}
