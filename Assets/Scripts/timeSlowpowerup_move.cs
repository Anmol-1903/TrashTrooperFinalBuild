using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeSlowpowerup_move : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
    }
}
