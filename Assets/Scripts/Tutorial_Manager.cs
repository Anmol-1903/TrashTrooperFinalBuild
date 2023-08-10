using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
    [SerializeField] GameObject Wet_Waste;
    [SerializeField] GameObject Dry_Waste;
    [SerializeField] GameObject _buttons;
    [SerializeField] GameObject _Catch_button;
    Trash trash_speed;

    private void Awake()
    {
        Wet_Waste = GameObject.FindGameObjectWithTag("WetWaste");
        Dry_Waste = GameObject.FindGameObjectWithTag("DryWaste");
        _buttons = GameObject.Find("ButtonInput");
        _Catch_button = GameObject.Find("catch_btn");
    }
    private void Start()
    {
        Wet_Waste.SetActive(false);
        Dry_Waste.SetActive(true);
        _buttons.SetActive(false);
        _Catch_button.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DryWaste"))
        {
            trash_speed = other.gameObject.GetComponent<Trash>();
            trash_speed.normalSpeed = 0;
            _buttons.SetActive(true);
        }
    }
}
