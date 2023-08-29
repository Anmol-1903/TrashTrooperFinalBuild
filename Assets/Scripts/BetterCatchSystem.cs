using TMPro;
using UnityEngine;
using System.Collections;
public class BetterCatchSystem : MonoBehaviour
{
    [SerializeField] Cannon _wet_Cannon;
    [SerializeField] Cannon _dry_Cannon;

    [SerializeField] AudioClip _wet_Trash_Dispose;
    [SerializeField] AudioClip _dry_Trash_Dispose;

    [SerializeField] GameObject _wet_Dustbin;
    [SerializeField] GameObject _dry_Dustbin;

    [SerializeField] TextMeshProUGUI _wet_Text;
    [SerializeField] TextMeshProUGUI _dry_Text;

    [SerializeField] Animator _wet_Dustbin_Lid;
    [SerializeField] Animator _dry_Dustbin_Lid;

    float _trash_Animation_Duration = 1f;
    float _trash_Animation_Height = 1f;

    [SerializeField] AnimationCurve throwCurve;

    Transform _wet_Dustbin_Trans;
    Transform _dry_Dustbin_Trans;

    [SerializeField] float _distance = 1f;

    bool _glove_enabled;

    bool _dry_Waste_In_Trigger;
    bool _wet_Waste_In_Trigger;
    bool _dry_Waste_In_Inventory;
    bool _wet_Waste_In_Inventory;

    [SerializeField] GameObject _trash_In_Inventory;

    [SerializeField] int _max_Dry_capacity = 3;
    [SerializeField] int _max_Wet_capacity = 3;
    
    [HideInInspector] public int _dry_capacity = 3;
    [HideInInspector] public int _wet_capacity = 3;
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
        _glove_enabled = playerMovement.glovePower;
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
                    if(_wet_Cannon != null)
                    {
                        _wet_Cannon.CollectTrash(_max_Wet_capacity - _wet_capacity);
                    }
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
                    if(_dry_Cannon != null)
                    {
                        _dry_Cannon.CollectTrash(_max_Dry_capacity - _dry_capacity);
                    }
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
        if (other.CompareTag("DryWaste") || other.CompareTag("Trashes"))
        {
            if (_dry_capacity > 0)
            {
                _dry_Waste_In_Trigger = true;
                _trash_In_Inventory = other.gameObject;
            }
        }
        else if (other.CompareTag("WetWaste") || other.CompareTag("Trashes"))
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
        if (other.CompareTag("DryWaste") || other.CompareTag("Trashes"))
        {
            if (_dry_capacity > 0)
            {
                _dry_Waste_In_Trigger = false;
            }
        }
        else if (other.CompareTag("WetWaste") || other.CompareTag("Trashes"))
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
            if (_glove_enabled)
            {
                _trash_Animation_Duration = Vector3.Distance(_trash_In_Inventory.transform.position, _wet_Dustbin.transform.position)/15;
                _trash_Animation_Height = Vector3.Distance(_trash_In_Inventory.transform.position, _wet_Dustbin.transform.position)/5;
                StartCoroutine(StartTrashAnimation(_trash_In_Inventory.transform.position, _wet_Dustbin.transform, _trash_In_Inventory, _wet_Trash_Dispose, true));
            }
            else
            {
                _wet_capacity--;
                _wet_Waste_In_Trigger = false;
                _wet_Waste_In_Inventory = true;
                AudioManager.Instance.TrashCollect();
                Destroy(_trash_In_Inventory);
            }
        }
        else if (_dry_Waste_In_Trigger)
        {
            if (_glove_enabled)
            {
                _trash_Animation_Duration = Vector3.Distance(_trash_In_Inventory.transform.position, _dry_Dustbin.transform.position)/15;
                _trash_Animation_Height = Vector3.Distance(_trash_In_Inventory.transform.position, _dry_Dustbin.transform.position)/5;
                StartCoroutine(StartTrashAnimation(_trash_In_Inventory.transform.position, _dry_Dustbin.transform, _trash_In_Inventory, _dry_Trash_Dispose, false));
            }
            else
            {
                _dry_capacity--;
                _dry_Waste_In_Trigger = false;
                _dry_Waste_In_Inventory = true;
                AudioManager.Instance.TrashCollect();
                Destroy(_trash_In_Inventory);
            }
        }
    }
    IEnumerator StartTrashAnimation(Vector3 startPosition, Transform targetPosition, GameObject _trash, AudioClip _trash_Dispose_Clip, bool isWet)
    {
        float timePassed = 0f;
        Vector3 throwDirection = (targetPosition.position - startPosition).normalized;
        float distance = Vector3.Distance(startPosition, targetPosition.position);
        float throwForce = distance / _trash_Animation_Duration;
        while (timePassed < _trash_Animation_Duration)
        {
            float normalizedTime = timePassed / _trash_Animation_Duration;
            float yOffset = throwCurve.Evaluate(normalizedTime) * _trash_Animation_Height;
            if (_trash != null)
            {
                targetPosition.GetComponentInParent<Animator>().SetBool("Open", true);
                _trash.transform.position = startPosition + throwDirection * throwForce * timePassed + Vector3.up * yOffset;
                timePassed += Time.deltaTime;
                if (Vector3.Distance(_trash.transform.position, targetPosition.position) < 1f)
                {
                    AudioManager.Instance.TrashDispose(_trash_Dispose_Clip);
                    yield return new WaitForSecondsRealtime(0.5f);
                    if (Vector3.Distance(transform.position, targetPosition.position) > 5f)
                    {
                        Debug.Log("NIKAL LAWDE");
                        targetPosition.GetComponentInParent<Animator>().SetBool("Open", false);
                    }
                    Destroy(_trash);
                    if (isWet)
                    {
                        if (_wet_Cannon != null)
                        {
                            _wet_Cannon.CollectTrash(1);
                        }
                    }
                    else
                    {
                        if (_dry_Cannon != null)
                        {
                            _dry_Cannon.CollectTrash(1);
                        }
                    }
                }
            }
            yield return null;
        }
    }
}