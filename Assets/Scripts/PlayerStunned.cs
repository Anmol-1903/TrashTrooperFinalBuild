using System.Collections;
using UnityEngine;

public class PlayerStunned : MonoBehaviour
{
    PlayerMove playerStunning;
    BossAI _aunty;
    [SerializeField] float _stunningTime;

    private void Awake()
    {
        playerStunning = GetComponent<PlayerMove>();
        _aunty = FindAnyObjectByType<BossAI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pot"))
        {
            if (_aunty)
            {
                _aunty.AuntyLaugh();
            }
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
