using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;
using System;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] AudioClip powerup_collected;
    [SerializeField] float speed;
    [SerializeField] public float fastSpeed;
    [SerializeField] float glovePower_timer = 5f;
    [SerializeField] float TimeSlowertimer = 10f;
    [SerializeField] float Magnettimer = 10f;
    [SerializeField] float TimeFastertimer = 10f;
    [SerializeField] Slider controller;

    [SerializeField] Slider _capTimer;
    [SerializeField] Slider _gloveTimer;
    [SerializeField] Slider _magnetTimer;
    float glove_counter;
    float cap_counter;
    float magnet_counter;


    [SerializeField] GameObject Gloves;
    [SerializeField] GameObject Hat;

    public GameObject ButtonInput , catchButton;
    [SerializeField] GameObject SliderInput;
    [Header("Higher = Harder press")]
    [Range(0, 1)]
    [SerializeField] float _sliderMoveDistance;

    Animator _uncleController;
    float current_speed;
    Transform wetClamp;
    Transform dryClamp;
    [HideInInspector] public bool glovePower;
    [HideInInspector] public bool timeSlowerPower;
    [HideInInspector] public bool timefaster;
    [HideInInspector] public bool magnetPowerupActive;

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
        if (_capTimer != null)
            _capTimer.gameObject.SetActive(false);
        if (_gloveTimer != null)
            _gloveTimer.gameObject.SetActive(false);
        if (_magnetTimer != null)
            _magnetTimer.gameObject.SetActive(false);
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
        if (PlayerPrefs.GetInt("SelectedPowerUp") == 2)
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
                    isRighrunning = true;
                    isLeftrunning = false;
                    transform.eulerAngles = new Vector3(0, -90f, 0);
                    _uncleController.SetBool("isRunning", true);
                }
                else if (controller.value < -_sliderMoveDistance)
                {
                    isLeftrunning = true;
                    isRighrunning = false;
                    transform.position += new Vector3(-current_speed * Time.deltaTime, 0, 0);
                    transform.eulerAngles = new Vector3(0, 90f, 0);
                    _uncleController.SetBool("isRunning", true);
                }
                else
                {
                    isLeftrunning = false;
                    isRighrunning = false;
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
        if (glovePower)
        {
            if (_gloveTimer != null)
                _gloveTimer.value = glove_counter;
            glove_counter -= Time.deltaTime;
        }
        if (timeSlowerPower)
        {
            if (_capTimer != null)
                _capTimer.value = cap_counter;
            cap_counter -= Time.deltaTime;
        }
        if (magnetPowerupActive)
        {
            if (_magnetTimer != null)
                _magnetTimer.value = magnet_counter;
            magnet_counter -= Time.deltaTime;
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
        if (Vector3.Distance(transform.position, wetClamp.position) < .25f || Vector3.Distance(transform.position, dryClamp.position) < .25f)
        {
            transform.eulerAngles = Vector3.zero;
            _uncleController.SetBool("isRunning", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("glove_powerup"))
        {
            StopCoroutine(gloveTimer());
            StartCoroutine(gloveTimer());
            Gloves.SetActive(true);
            AudioManager.Instance.PowerupCollect(powerup_collected);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("cap_powerup"))
        {
            StopCoroutine(TimeSlowerTimer());
            StartCoroutine(TimeSlowerTimer());
            Hat.SetActive(true);
            AudioManager.Instance.PowerupCollect(powerup_collected);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Time_slower"))
        {
            StopCoroutine(TimeFasterTimer());
            StartCoroutine(TimeFasterTimer());
            AudioManager.Instance.PowerupCollect(powerup_collected);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("magnet_powerup"))
        {
            StopCoroutine(MagnetTimer());
            StartCoroutine(MagnetTimer());
            AudioManager.Instance.PowerupCollect(powerup_collected);
            Destroy(other.gameObject);
        }
    }

    IEnumerator MagnetTimer()
    {
        if (_magnetTimer != null)
            _magnetTimer.gameObject.SetActive(true);
        magnet_counter = Magnettimer;
        if (_magnetTimer != null)
            _magnetTimer.maxValue = Magnettimer;
        magnetPowerupActive = true;
        yield return new WaitForSeconds(Magnettimer);
        magnetPowerupActive = false;
        if (_magnetTimer != null)
            _magnetTimer.gameObject.SetActive(false);
    }

    IEnumerator gloveTimer()
    {
        if (_gloveTimer != null)
            _gloveTimer.gameObject.SetActive(true);
        glove_counter = glovePower_timer;
        if (_gloveTimer != null)
            _gloveTimer.maxValue = glovePower_timer;
        glovePower = true;
        yield return new WaitForSeconds(glovePower_timer);
        glovePower = false;
        if (_gloveTimer != null)
            _gloveTimer.gameObject.SetActive(false);
        Gloves.SetActive(false);
    }
    IEnumerator TimeSlowerTimer()
    {
        if (_capTimer != null)
            _capTimer.gameObject.SetActive(true);
        cap_counter = TimeSlowertimer;
        if (_capTimer != null)
            _capTimer.maxValue = TimeSlowertimer;
        timeSlowerPower = true;
        yield return new WaitForSeconds(TimeSlowertimer);
        timeSlowerPower = false;
        if (_capTimer != null)
        _capTimer.gameObject.SetActive(false);
            Hat.SetActive(false);
    }
    IEnumerator TimeFasterTimer()
    {
        timefaster = true;
        yield return new WaitForSeconds(TimeFastertimer);
        timefaster = false;
    }
    public void SliderPointerUp()
    {
        if (controller != null)
        {
            controller.value = 0f;
        }
    }
}