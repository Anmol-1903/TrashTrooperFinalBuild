using UnityEngine;
public class BossAI : MonoBehaviour
{
    Animator _anim;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] GameObject[] _trashPrefabs;
    [SerializeField] float _lowHealth = 30f;
    GameObject _selectedTrashPrefab;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    public void ThrowTrash()
    {
        _selectedTrashPrefab = _trashPrefabs[Random.Range(0, _trashPrefabs.Length)];
        if(Random.Range(0,2) == 0)
        {
            Instantiate(_selectedTrashPrefab, _spawnPoint.position, Quaternion.identity);
        }
    }
    public void AuntyDamage(bool left, bool dead)
    {
        if (left)
        {
            _anim.SetTrigger("HurtLeft");
        }
        else
        {
            _anim.SetTrigger("HurtRight");
        }
        _anim.SetBool("Dead", dead);
    }
    public void NextLevelPanelAnimation()
    {
        PauseMenu.Instance.CallLvlComplete();
    }
    public void AuntyLaugh()
    {
        _anim.SetTrigger("PlayerHurt");
    }
    public void AuntyVictory()
    {
        _anim.SetTrigger("AuntyWin");
    }
    public void RestartPanel()
    {
        GameManager _gm = FindObjectOfType<GameManager>();
        _gm.RestartPanelActive();
    }
    public void AuntyLow()
    {
        _anim.SetBool("AuntyLow", true);
    }
    public void AuntySmashBuilding()
    {
        FindObjectOfType<CameraShake>().ShakeCamera();
    }
    public void AuntyMad()
    {
        if(FindObjectOfType<BossHealth>().GetCurrentHealth() <= _lowHealth)
        {
            AuntyLow();
        }
    }
}