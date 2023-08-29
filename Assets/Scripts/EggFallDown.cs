using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggFallDown : MonoBehaviour
{
    public float _speed;
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddTorque(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        }
    }

    private void Update()
    {
        transform.position += new Vector3(0f, -_speed * Time.deltaTime, 0f);
    }
}
