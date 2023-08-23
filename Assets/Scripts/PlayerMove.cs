using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] AudioClip powerup_collected;
    [SerializeField] float speed;
    [SerializeField] float fastSpeed;
    [SerializeField] float glovePower_timer = 5f;
    [SerializeField] float TimeSlowertimer = 10f;
    [SerializeField] GameObject playerTrans;
    [SerializeField] Vector3 offset;
    [SerializeField] Slider controller;

    [SerializeField] Animator _uncleController;

    [SerializeField] GameObject Gloves;
    [SerializeField] GameObject Hat;

    [SerializeField] GameObject ButtonInput;
    [SerializeField] GameObject SliderInput;
    [Header("Higher = Harder press")]
    [Range(0,1)]
    [SerializeField] float _sliderMoveDistance;

    float current_speed;
    Transform wetClamp;
    Transform dryClamp;
    public bool glovePower;
    public bool timeSlowerPower;

    public bool isRighrunning;
    public bool isLeftrunning;
    private void Awake()
    {
        Gloves = GameObject.FindGameObjectWithTag("Gloves");
        Hat = GameObject.FindGameObjectWithTag("Cap");
    }
    private void Start()
    {
        Gloves.SetActive(false);
        Hat.SetActive(false);
        Time.timeScale = 1f;
    }
    private void OnEnable()
    {
        wetClamp = GameObject.FindGameObjectWithTag("WetClamp").transform;
        dryClamp = GameObject.FindGameObjectWithTag("DryClamp").transform;
        if (PlayerPrefs.GetInt("controllerType") == 1)
        {
            SliderInput.SetActive(true);
            ButtonInput.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("controllerType") == 0)
        {
            SliderInput.SetActive(false);
            ButtonInput.SetActive(true);
        }
        _uncleController = GetComponentInChildren<Animator>();
        if(PlayerPrefs.GetInt("SelectedPowerUp") == 2)
        {
            current_speed = fastSpeed;
        }
        else
        {
            current_speed = speed;
        }
    }
    public void rightBtnDown()
    {
        isRighrunning = true;
    }
    public void rightBtnUp()
    {
        isRighrunning = false;
    }
    public void leftBtnDown()
    {
        isLeftrunning = true;
    }
    public void leftBtnUp()
    {
        isLeftrunning = false;
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("controllerType") == 1)
        {
            if (controller != null)
            {
                if (controller.value > _sliderMoveDistance)
                {
                    transform.position += new Vector3(current_speed * Time.deltaTime, 0, 0);
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                    _uncleController.SetBool("isRunning", true);
                }
                else if (controller.value < -_sliderMoveDistance)
                {
                    transform.position += new Vector3(-current_speed * Time.deltaTime, 0, 0);
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                    _uncleController.SetBool("isRunning", true);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    _uncleController.SetBool("isRunning", false);
                }
            }
            else
            {
                PlayerMovement();
            }
        }
        else if (PlayerPrefs.GetInt("controllerType") == 0)
        {
            PlayerMovement();
        }
        Clamper();
    }
    void PlayerMovement()
    {
        if (isRighrunning)
        {
            transform.position += new Vector3(current_speed * Time.deltaTime, 0, 0);
            if ((Vector3.Distance(transform.position, dryClamp.position) > .1f))
            {
                transform.eulerAngles = new Vector3(0, -90f, 0);
                _uncleController.SetBool("isRunning", true);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                _uncleController.SetBool("isRunning", false);
            }
        }
        else if (isLeftrunning)
        {
            transform.position += new Vector3(-current_speed * Time.deltaTime, 0, 0);
            if ((Vector3.Distance(transform.position, wetClamp.position) > .1f))
            {
                transform.eulerAngles = new Vector3(0, 90f, 0);
                _uncleController.SetBool("isRunning", true);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                _uncleController.SetBool("isRunning", false);
            }
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            _uncleController.SetBool("isRunning", false);
        }
    }
    void Clamper()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, wetClamp.position.x, dryClamp.position.x), transform.position.y, 0);
        if(Vector3.Distance(playerTrans.transform.position, wetClamp.position) < .25f || Vector3.Distance(playerTrans.transform.position, dryClamp.position) < .25f)
        {
            transform.eulerAngles = Vector3.zero;
            _uncleController.SetBool("isRunning", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("glove_powerup"))
        {
            StartCoroutine(gloveTimer());
            Gloves.SetActive(true);
            AudioManager.Instance.PowerupCollect(powerup_collected);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("cap_powerup"))
        {
            StartCoroutine(TimeSlowerTimer());
            Hat.SetActive(true);
            AudioManager.Instance.PowerupCollect(powerup_collected);
            Destroy(other.gameObject);
        }
    }
    IEnumerator gloveTimer()
    {
        glovePower = true;
        yield return new WaitForSeconds(glovePower_timer);
        glovePower = false;
        Gloves.SetActive(false);
    }
    IEnumerator TimeSlowerTimer()
    {
        timeSlowerPower = true;
        yield return new WaitForSeconds(TimeSlowertimer);
        timeSlowerPower = false;
        Hat.SetActive(false);
    }
    public void SliderPointerUp()
    {
        if (controller != null)
        {
            controller.value = 0f;
        }
    }
}