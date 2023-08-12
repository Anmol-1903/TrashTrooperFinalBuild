using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class catch_system : MonoBehaviour
{
    //Private variables
    bool wetWasteInTrigger;
    bool dryWasteInTrigger;
    bool drywasteInventory;
    bool wetwasteInventory;
    int wetCollect;
    int dryCollect;
    public int current_capacity;
    public int capacity = 2;
    public bool glovePower;
    PlayerMove playerMovement;
    GameObject wetWaste;
    GameObject dryWaste;

    //SerializeField variables
    [SerializeField] Slider controller;
    [SerializeField] int default_Capacity;
    [SerializeField] int upgraded_Capacity;
    [SerializeField] float distance = 5f;
    [Header("---------Texts---------")]
    [SerializeField] TextMeshProUGUI _capacityText;
    [SerializeField] TextMeshProUGUI dryWasteText;
    [SerializeField] TextMeshProUGUI wetWasteText;
    [SerializeField] TextMeshProUGUI capacity_txt;
    [SerializeField] TextMeshProUGUI picker_txt;
    [Header("------GameObjects------")]
    [SerializeField] Transform wetDustBinTrans;
    [SerializeField] Transform dryDustBinTrans;
    [SerializeField] GameObject playerTrans;


    private void Awake()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player");
        wetDustBinTrans = GameObject.FindGameObjectWithTag("WetClamp").transform;
        dryDustBinTrans = GameObject.FindGameObjectWithTag("DryClamp").transform;
        playerMovement = playerTrans.GetComponent<PlayerMove>();
    }
    private void Start()
    {
        if (picker_txt != null)
        {
            picker_txt.enabled = false;
        }
        if (capacity_txt != null)
        {
            capacity_txt.enabled = false;
        }
        if (PlayerPrefs.GetInt("SelectedPowerUp") == 1)
        {
            capacity = upgraded_Capacity;
        }
        else
        {
            capacity = default_Capacity;
        }
        current_capacity = capacity;
    }
    private void Update()
    {
        if (Vector3.Distance(playerTrans.transform.position, wetDustBinTrans.position) < distance && playerMovement.isLeftrunning)
        {
            if (wetwasteInventory)
            {
                wetCollect = wetCollect + capacity - current_capacity;
                wetwasteInventory = false;
                current_capacity = capacity;
                if (wetWasteText != null)
                {
                    wetWasteText.text = "Wet: " + (wetCollect).ToString();
                }
            }
        }
        else if (Vector3.Distance(playerTrans.transform.position, dryDustBinTrans.position) < distance && playerMovement.isRighrunning)
        {
            if (drywasteInventory)
            {
                dryCollect += capacity - current_capacity;
                drywasteInventory = false;
                current_capacity = capacity;
                if (dryWasteText != null)
                {
                    dryWasteText.text = "Dry: " + (dryCollect).ToString();
                }
            }
        }
        this.glovePower = playerMovement.glovePower;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WetWaste"))
        {
            wetWaste = other.gameObject;
            wetWasteInTrigger = true;
        }
        if (other.gameObject.CompareTag("DryWaste"))
        {
            dryWaste = other.gameObject;
            dryWasteInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WetWaste"))
        {
            wetWasteInTrigger = false;
        }
        if (other.gameObject.CompareTag("DryWaste"))
        {
            dryWasteInTrigger = false;
        }
    }
    public void CatchTrash()
    {
        if (wetWasteInTrigger)
        {
            if (current_capacity > 0 && !drywasteInventory)
            {
                Destroy(wetWaste);
                wetWasteInTrigger = false;
                current_capacity--;
                wetwasteInventory = true;
                if (glovePower)
                {
                    if (wetwasteInventory)
                    {
                        current_capacity = capacity;
                        wetwasteInventory = false;
                        wetCollect++;
                        if (wetWasteText != null)
                        {
                            wetWasteText.text = "Wet: " + (wetCollect).ToString();
                        }
                    }
                }
            }
            else if (current_capacity > 0 && drywasteInventory)
            {
                StartCoroutine(waiting());
            }
            else
            {
                if (capacity_txt != null)
                {
                    capacity_txt.enabled = true;
                }
            }
        }
        else if (dryWasteInTrigger)
        {
            if (current_capacity > 0 && !wetwasteInventory)
            {
                Destroy(dryWaste);
                dryWasteInTrigger = false;
                current_capacity--;
                drywasteInventory = true;
                if (glovePower)
                {
                    if (drywasteInventory)
                    {
                        current_capacity = capacity;
                        drywasteInventory = false;
                        dryCollect++;
                        if (dryWasteText != null)
                        {
                            dryWasteText.text = "Dry: " + (dryCollect).ToString();
                        }
                    }
                }
            }
            else if (current_capacity > 0 && wetwasteInventory)
            {
                StartCoroutine(waiting());
            }
            else
            {
                if (capacity_txt != null)
                {
                    capacity_txt.enabled = true;
                }
            }
        }
    }
    IEnumerator waiting()
    {
        if (picker_txt != null)
        {
            picker_txt.enabled = true;
            yield return new WaitForSeconds(3f);
            picker_txt.enabled = false;
        }
    }
}