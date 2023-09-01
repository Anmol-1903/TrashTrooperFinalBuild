using System.Collections;
using UnityEngine;

public class PlayerStunned : MonoBehaviour
{
    PlayerMove playerStunning;
    [SerializeField] float _stunningTime;

    private void Awake()
    {
        playerStunning = GetComponent<PlayerMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pot"))
        {
            Destroy(other.gameObject);
            playerStunning.ButtonInput.SetActive(false);
            playerStunning.catchButton.SetActive(false);
            playerStunning.isLeftrunning = false;
            playerStunning.isRighrunning = false;
            StartCoroutine(PlayerStuned());
        }
    }
    IEnumerator PlayerStuned()
    {
        yield return new WaitForSeconds(_stunningTime);
        playerStunning.ButtonInput.SetActive(true);
        playerStunning.catchButton.SetActive(true);
    }
}
