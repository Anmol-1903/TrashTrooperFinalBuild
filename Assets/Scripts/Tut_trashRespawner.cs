using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_trashRespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trashes"))
        {
            other.gameObject.transform.position = new Vector3(Random.Range(-8.5f,9.3f) , Random.Range(16f,35f), 0);
        }
    }
}
