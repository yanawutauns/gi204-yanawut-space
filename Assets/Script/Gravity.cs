using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.0667f;
    public static List<Gravity> gbList;

    [SerializeField] bool planet = false;
    [SerializeField]int orbitSpeed = 1000;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gbList == null)
        {
            gbList = new List<Gravity>();
        }
        gbList.Add(this);

        if (planet)
        {
            rb.AddForce(Vector3.left * orbitSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in gbList)
        {
            if(obj != this)
            Attract(obj);
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float forceManitude = G * (rb.mass * otherRb.mass/Mathf.Pow(distance,2));
        Vector3 gravityForce = forceManitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }
}
