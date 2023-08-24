using TMPro;
using UnityEngine;
public class BetterCatchSystem : MonoBehaviour
{
    [SerializeField] AudioClip _wet_Trash_Dispose;
    [SerializeField] AudioClip _dry_Trash_Dispose;

    [SerializeField] TextMeshProUGUI _wet_Text;
    [SerializeField] TextMeshProUGUI _dry_Text;

    [SerializeField] Animator _wet_Dustbin_Lid;
    [SerializeField] Animator _dry_Dustbin_Lid;

    Transform _wet_Dustbin_Trans;
    Transform _dry_Dustbin_Trans;

    [SerializeField] float _distance = 1f;

    bool _dry_Waste_In_Trigger;
    bool _wet_Waste_In_Trigger;
    bool _dry_Waste_In_Inventory;
    bool _wet_Waste_In_Inventory;

    [SerializeField] GameObject _trash_In_Inventory;

    [SerializeField] int _max_Dry_capacity = 3;
    [SerializeField] int _max_Wet_capacity = 3;
    
    public int _dry_capacity = 3;
    public int _wet_capacity = 3;
    int _capacity_upgrade;

    TrashDeSpawner floor;
    PlayerMove playerMovement;


    private void Awake()
    {
        _wet_Dustbin_Trans = GameObject.FindGameObjectWithTag("WetClamp").transform;
        _dry_Dustbin_Trans = GameObject.FindGameObjectWithTag("DryClamp").transform;
        floor = FindObjectOfType<TrashDeSpawner>();
        playerMovement = GetComponent<PlayerMove>();
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("SelectedPowerUp") == 1)
        {
            _capacity_upgrade = 2;
        }
        else
        {
            _capacity_upgrade = 1;
        }
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _wet_Dustbin_Trans.position) < _distance)
        {
            if (playerMovement.isLeftrunning)
            {
                if (_wet_Dustbin_Lid != null)
                    _wet_Dustbin_Lid.SetBool("Open", true);
                if (_wet_Waste_In_Inventory)
                {
                    _wet_Waste_In_Inventory = false;
                    AudioManager.Instance.TrashDispose(_wet_Trash_Dispose);
                    floor.HealNature(_max_Wet_capacity - _wet_capacity);
                    _wet_capacity = _max_Wet_capacity * _capacity_upgrade;
                }
            }
        }
        else
        {
            if (_wet_Dustbin_Lid != null)
                _wet_Dustbin_Lid.SetBool("Open", false);
        }
        if (Vector3.Distance(transform.position, _dry_Dustbin_Trans.position) < _distance)
        {
            if (playerMovement.isRighrunning)
            {
                if (_dry_Dustbin_Lid != null)
                    _dry_Dustbin_Lid.SetBool("Open", true);
                if (_dry_Waste_In_Inventory)
                {
                    _dry_Waste_In_Inventory = false;
                    AudioManager.Instance.TrashDispose(_dry_Trash_Dispose);
                    floor.HealNature(_max_Dry_capacity - _dry_capacity);
                    _dry_capacity = _max_Dry_capacity * _capacity_upgrade;
                }
            }
        }
        else
        {
            if (_dry_Dustbin_Lid != null)
                _dry_Dustbin_Lid.SetBool("Open", false);
        }

        if (_wet_Text != null)
            _wet_Text.text = (_max_Wet_capacity - _wet_capacity).ToString() + "/" + (_max_Wet_capacity).ToString();
        if (_dry_Text != null)
            _dry_Text.text = (_max_Dry_capacity - _dry_capacity).ToString() + "/" + (_max_Dry_capacity).ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DryWaste"))
        {
            if (_dry_capacity > 0)
            {
                _dry_Waste_In_Trigger = true;
                _trash_In_Inventory = other.gameObject;
            }
        }
        else if (other.CompareTag("WetWaste"))
        {
            if (_wet_capacity > 0)
            {
                _wet_Waste_In_Trigger = true;
                _trash_In_Inventory = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DryWaste"))
        {
            if (_dry_capacity > 0)
            {
                _dry_Waste_In_Trigger = false;
            }
        }
        else if (other.CompareTag("WetWaste"))
        {
            if (_wet_capacity > 0)
            {
                _wet_Waste_In_Trigger = false;
            }
        }
    }
    public void CatchTrash()
    {
        if (_wet_Waste_In_Trigger)
        {
            _wet_capacity--;
            _wet_Waste_In_Trigger = false;
            _wet_Waste_In_Inventory = true;
            Destroy(_trash_In_Inventory);
        }
        else if (_dry_Waste_In_Trigger)
        {
            _dry_capacity--;
            _dry_Waste_In_Trigger = false;
            _dry_Waste_In_Inventory = true;
            Destroy(_trash_In_Inventory);
        }
    }
}