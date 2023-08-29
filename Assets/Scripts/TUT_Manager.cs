using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TUT_Manager : MonoBehaviour
{
    [SerializeField] GameObject Trash1, Trash2, Trash3;
    [SerializeField] GameObject move_UI;
    [SerializeField] GameObject catch_UI;
    [SerializeField] GameObject drywasteDeposite_UI, wetwasteDeposite_UI;
    [SerializeField] GameObject wetWaste_UI;
    [SerializeField] GameObject cleanlinessMeter_UI, taskUI;
    [SerializeField] Transform player, wetdustbin_Trans, drydustbin_Trans;
    [SerializeField] float wetWaste_speed, drywaste_speed;
    [SerializeField] BetterCatchSystem catch_system;
    [SerializeField] GameObject banana, egg, chips, chips2;
    [SerializeField] GameObject TutorialStarts, TutorialEnd, Inventory;
    PlayerMove playermove;
    // Start is called before the first frame update


    private void Awake()
    {
        playermove = GameObject.FindObjectOfType<PlayerMove>();
    }


    void Start()
    {
        Inventory.SetActive(false);
        TutorialEnd.SetActive(false);   
        move_UI.SetActive(true);
        catch_UI.SetActive(false);
        cleanlinessMeter_UI.SetActive(false);
        wetWaste_UI.SetActive(false);
        drywasteDeposite_UI.SetActive(false);
        wetwasteDeposite_UI.SetActive(false);
        taskUI.SetActive(false);
        Trash1.GetComponent<Trash>().normalSpeed = 0;
        Trash2.GetComponent<Trash>().normalSpeed = 0;
        Trash3.GetComponent<EggFallDown>()._speed = 0;

        banana.GetComponent<EggFallDown>()._speed = 0;
        egg.GetComponent<EggFallDown>()._speed = 0;
        chips.GetComponent<EggFallDown>()._speed = 0;
        chips2.GetComponent<EggFallDown>()._speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        CatchUI();
        if (Vector3.Distance(player.position, drydustbin_Trans.position) < 1f)
        {
            Destroy(drywasteDeposite_UI);
            if (Trash2 != null)
            {
                StartCoroutine(TimerForWetWasteUI());
            }
        }
        WetWasteUI();
        if (Vector3.Distance(player.position, wetdustbin_Trans.position) < 1f)
        {
            Destroy(wetwasteDeposite_UI);
            if (Trash2 == null)
            {
                if (Trash3 != null)
                {
                    Trash3.GetComponent<EggFallDown>()._speed = 10f;
                }
            }
        }
        CleanlinessMeterUI();

        if(banana == null && egg == null && chips == null && chips2 == null)
        {
            taskUI.SetActive(false);
            Inventory.SetActive(false);
            TutorialEnd.SetActive(true);
        }
    }

    void PlayerMove()
    {
        if (Trash1 != null)
        {
            Trash1.GetComponent<Trash>().normalSpeed = drywaste_speed;
            move_UI.SetActive(true);
            catch_UI.SetActive(true);
        }
        else if (Trash1 == null)
        {
            move_UI.SetActive(false);
        }

    }

    void CatchUI()
    {
        if (Trash1 == null)
        {
            catch_UI.SetActive(false);
            if (drywasteDeposite_UI != null)
            {
                drywasteDeposite_UI.SetActive(true);
            }
        }
    }
    IEnumerator TimerForWetWasteUI()
    {
        Trash2.GetComponent<Trash>().normalSpeed = 1.5f;
        yield return new WaitForSeconds(1f);
        Trash2.GetComponent<Trash>().normalSpeed = 0;
        wetWaste_UI.SetActive(true);

        yield return new WaitForSeconds(2f);
        wetWaste_UI.SetActive(false);
        Trash2.GetComponent<Trash>().normalSpeed = wetWaste_speed;
    }
    void WetWasteUI()
    {
        if (Trash2 == null)
        {
            if (wetwasteDeposite_UI != null)
            {
                wetwasteDeposite_UI.SetActive(true);
            }
        }
    }
    void CleanlinessMeterUI()
    {
        if (Trash3 == null)
        {
            if (cleanlinessMeter_UI != null)
            {
                cleanlinessMeter_UI.SetActive(true);
                StartCoroutine(TaskStartTimer());
            }
        }
    }
    IEnumerator TaskStartTimer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(cleanlinessMeter_UI);
        Inventory.SetActive(true);
        taskUI.SetActive(true);
        if (egg != null)
        {
            egg.GetComponent<EggFallDown>()._speed = 5f;
        }

        if (banana != null)
        {
            banana.GetComponent<EggFallDown>()._speed = 5f;

        }
        if(chips != null)
        {
            chips.GetComponent<EggFallDown>()._speed = 5f;
        }
        if(chips2 != null)
        {
            chips2.GetComponent<EggFallDown>()._speed = 5f;
        }
    }
}
