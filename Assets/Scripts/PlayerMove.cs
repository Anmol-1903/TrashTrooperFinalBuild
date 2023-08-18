using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float fastSpeed;
    [SerializeField] float glovePower_timer = 5f;
    [SerializeField] float TimeSlowertimer = 10f;
    [SerializeField] GameObject playerTrans;
    [SerializeField] Vector3 offset;
    [SerializeField] Slider controller;
    [SerializeField] Transform Target;

    [SerializeField] Animator _uncleController;

    [SerializeField] GameObject Gloves;
    [SerializeField] GameObject Hat;

    [SerializeField] GameObject ButtonInput;
    [SerializeField] GameObject SliderInput;

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
        _uncleController = GetComponent<Animator>();
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
            if (controller.value > 0.5f)
            {
                transform.position += new Vector3(current_speed * Time.deltaTime, 0, 0);
                transform.eulerAngles = new Vector3(0, -90f, 0);
                _uncleController.SetBool("isRunning", true);
            }
            else if (controller.value < -0.5f)
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
                transform.eulerAngles = new Vector3(0, 0, 0);
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
                transform.eulerAngles = new Vector3(0, 0, 0);
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, wetClamp.position.x, dryClamp.position.x), 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("gloves"))
        {
            StartCoroutine(gloveTimer());
            Gloves.SetActive(true);
            Hat.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("timeslower"))
        {
            StartCoroutine(TimeSlowerTimer());
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
        controller.value = 0f;
    }
}