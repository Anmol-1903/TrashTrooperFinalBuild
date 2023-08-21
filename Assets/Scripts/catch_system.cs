using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class catch_system : MonoBehaviour
{
    //Private variables
    bool wetWasteInTrigger;
    bool dryWasteInTrigger;
    [SerializeField] bool drywasteInventory;
    [SerializeField] bool wetwasteInventory;
    int wetCollect;
    int dryCollect;
    public int current_capacity;
    public int capacity = 2;
    public bool glovePower;
    PlayerMove playerMovement;
    GameObject wetWaste;
    GameObject dryWaste;

    [Header("from small to big")]
    [SerializeField] GameObject[] TrashBags;
    [Tooltip("trashbag size is increased after every __ trash collected")]
    [SerializeField] int _trashBagSize = 1;

    [SerializeField] Color defaultColor;
    [SerializeField] Color wetWasteColor;
    [SerializeField] Color dryWasteColor;

    //SerializeField variables
    [SerializeField] Image _bg;
    [SerializeField] Slider controller;
    [SerializeField] int default_Capacity;
    [SerializeField] int upgraded_Capacity;
    [SerializeField] float distance = 5f;
    [Header("---------Texts---------")]
    [SerializeField] TextMeshProUGUI _capacityText;
    [SerializeField] TextMeshProUGUI dryWasteText;
    [SerializeField] TextMeshProUGUI wetWasteText;
    [SerializeField] TextMeshProUGUI picker_txt;
    [SerializeField] TextMeshProUGUI _inventoryText;
    [Header("------GameObjects------")]
    [SerializeField] Transform wetDustBinTrans;
    [SerializeField] Transform dryDustBinTrans;
    [SerializeField] GameObject playerTrans;

    [SerializeField] AudioClip _wetTrashDispose;
    [SerializeField] AudioClip _dryTrashDispose;


    [SerializeField] Animator _dryDustbinLid;
    [SerializeField] Animator _wetDustbinLid;

    private void Awake()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player");
        wetDustBinTrans = GameObject.FindGameObjectWithTag("WetClamp").transform;
        dryDustBinTrans = GameObject.FindGameObjectWithTag("DryClamp").transform;
        _wetDustbinLid = GameObject.FindGameObjectWithTag("WetClamp").GetComponentInParent<Animator>();
        _dryDustbinLid = GameObject.FindGameObjectWithTag("DryClamp").GetComponentInParent<Animator>();
        playerMovement = playerTrans.GetComponent<PlayerMove>();
    }
    private void Start()
    {
        if (picker_txt != null)
        {
            picker_txt.enabled = false;
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
        this.glovePower = playerMovement.glovePower;
        foreach (var trashBag in TrashBags)
        {
            trashBag.SetActive(false);
        }
        if (TrashBags[(capacity - current_capacity) / _trashBagSize] != null)

            TrashBags[(capacity - current_capacity) / _trashBagSize].SetActive(true);
    }
    private void LateUpdate()
    {
        if (_inventoryText != null)
        {
            _inventoryText.text = (capacity - current_capacity).ToString() + "/" + (capacity).ToString();
        }
        if (_bg != null)
        {
            if (wetwasteInventory)
            {
                _bg.color = Color.Lerp(_bg.color, wetWasteColor, Time.deltaTime);
            }
            else if (drywasteInventory)
            {
                _bg.color = Color.Lerp(_bg.color, dryWasteColor, Time.deltaTime);
            }
            else
            {
                _bg.color = Color.Lerp(_bg.color, defaultColor, Time.deltaTime);
            }
        }
    }
    private void Update()
    {
        if (Vector3.Distance(playerTrans.transform.position, wetDustBinTrans.position) < distance)
        {
            _wetDustbinLid.SetBool("Open", true);
            if (playerMovement.isLeftrunning)
            {
                if (wetwasteInventory)
                {
                    wetCollect = wetCollect + capacity - current_capacity;
                    wetwasteInventory = false;
                    current_capacity = capacity;
                    AudioManager.Instance.TrashDispose(_wetTrashDispose);
                    foreach (var trashBag in TrashBags)
                    {
                        trashBag.SetActive(false);
                    }
                    if (TrashBags[(capacity - current_capacity) / _trashBagSize] != null)
                        TrashBags[(capacity - current_capacity) / _trashBagSize].SetActive(true);
                    if (wetWasteText != null)
                    {
                        wetWasteText.text = "Wet: " + (wetCollect).ToString();
                    }
                }
            }
        }
        else
        {
            _wetDustbinLid.SetBool("Open", false);
        }
        if (Vector3.Distance(playerTrans.transform.position, dryDustBinTrans.position) < distance)
        {
            _dryDustbinLid.SetBool("Open", true);
            if (playerMovement.isRighrunning)
            {
                if (drywasteInventory)
                {
                    dryCollect += capacity - current_capacity;
                    drywasteInventory = false;
                    current_capacity = capacity;
                    AudioManager.Instance.TrashDispose(_dryTrashDispose);
                    foreach (var trashBag in TrashBags)
                    {
                        trashBag.SetActive(false);
                    }
                    if (TrashBags[(capacity - current_capacity) / _trashBagSize] != null)
                        TrashBags[(capacity - current_capacity) / _trashBagSize].SetActive(true);
                    if (dryWasteText != null)
                    {
                        dryWasteText.text = "Dry: " + (dryCollect).ToString();
                    }
                }
            }
        }
        else
        {
            _dryDustbinLid.SetBool("Open", false);
        }
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
                foreach (var trashBag in TrashBags)
                {
                    trashBag.SetActive(false);
                }
                if (TrashBags[(capacity - current_capacity) / _trashBagSize] != null)
                    TrashBags[(capacity - current_capacity) / _trashBagSize].SetActive(true);
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
          
        }
        else if (dryWasteInTrigger)
        {
            if (current_capacity > 0 && !wetwasteInventory)
            {
                Destroy(dryWaste);
                foreach (var trashBag in TrashBags)
                {
                    trashBag.SetActive(false);
                }
                if (TrashBags[(capacity - current_capacity) / _trashBagSize] != null)
                    TrashBags[(capacity - current_capacity) / _trashBagSize].SetActive(true);
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