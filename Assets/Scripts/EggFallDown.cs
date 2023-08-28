using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggFallDown : MonoBehaviour
{
    [SerializeField] GameObject cleanliness_meterUI;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Egg_Collider"))
        {
            cleanliness_meterUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
