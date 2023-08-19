using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioClip bg_clip;
    [SerializeField] AudioSource bg_music;
    [SerializeField] AudioSource trash_falldown;
    [SerializeField] AudioSource powerup_collect;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        BG_Music(bg_clip);
    }
    public void BG_Music(AudioClip music)
    {
        bg_music.clip = music;
        bg_music.Play();
    }
    public void TrashFallDown(AudioClip music)
    {
        trash_falldown.clip = music;
        trash_falldown.Play();
    }
    public void PowerupCollect(AudioClip music)
    {
        powerup_collect.clip = music;
        powerup_collect.Play();
    }
}