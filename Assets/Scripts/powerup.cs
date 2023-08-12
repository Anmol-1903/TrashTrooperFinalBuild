using UnityEngine;

public class powerup : MonoBehaviour
{
    [SerializeField] GameObject gloves_;
    [SerializeField] GameObject timeSlower;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(gloves_,transform.position,Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(timeSlower, transform.position, Quaternion.identity);
        }
    }
}
