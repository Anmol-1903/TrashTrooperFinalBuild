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

    [SerializeField] GameObject ButtonInput;
    [SerializeField] GameObject SliderInput;


    float current_speed;
    Transform wetClamp;
    Transform dryClamp;
    Transform gloveTrans;
    public bool glovePower;
    public bool timeSlowerPower;

    public bool isRighrunning;
    public bool isLeftrunning;
    private void Start()
    {
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
            Target.position = Vector3.Lerp(wetClamp.position, dryClamp.position, controller.value);
            if (Vector3.Distance(transform.position, Target.position) > .1f && Time.timeScale == 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.position, current_speed / 200);
                if ((Vector3.Distance(transform.position, dryClamp.position) > .1f) || (Vector3.Distance(transform.position, wetClamp.position) > .1f))
                {
                    _uncleController.SetBool("isRunning", true);
                }
            }
            else
            {
                _uncleController.SetBool("isRunning", false);
            }
            Rotate();
        }
        else if(PlayerPrefs.GetInt("controllerType") == 0)
        {
            PlayerMovement();   
            Clamper();
        }
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
   /* private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("gloves"))
        {

            StartCoroutine(gloveTimer());
            gloves_Move = other.gameObject.GetComponent<gloves_move>();
            gloveTrans = other.gameObject.GetComponent<Transform>();
            gloveTrans.parent = playerTrans.transform;
            gloveTrans.position = playerTrans.transform.position + offset;
            gloves_Move.speed = 0;
            Debug.Log(other.gameObject.name);
        }
        if (other.gameObject.CompareTag("timeslower"))
        {
            StartCoroutine(TimeSlowerTimer());
            Destroy(other.gameObject);
        }
    }*/
    IEnumerator gloveTimer()
    {
        glovePower = true;
        yield return new WaitForSeconds(glovePower_timer);
        glovePower = false;
        Destroy(gloveTrans.gameObject);
    }
    IEnumerator TimeSlowerTimer()
    {
        timeSlowerPower = true;
        yield return new WaitForSeconds(TimeSlowertimer);
        timeSlowerPower = false;
    }
    void Rotate()
    {
        if (Vector3.Distance(transform.position, Target.position) > .1f && transform.position.x < Target.position.x)
        {
            isRighrunning = true;
            isLeftrunning = false;
            transform.eulerAngles = new Vector3(0, -90f, 0);
        }
        else if (Vector3.Distance(transform.position, Target.position) > .1f && transform.position.x > Target.position.x)
        {
            isLeftrunning = true;
            isRighrunning = false;
            transform.eulerAngles = new Vector3(0, 90f, 0);
        }
        else
        {
            isRighrunning = false;
            isLeftrunning = false;
            transform.eulerAngles = Vector3.zero;
        }
    }


}