using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioClip bg_clip;
    [SerializeField] AudioSource bg_music;
    [SerializeField] AudioSource trash_falldown;
    [SerializeField] AudioSource powerup_collect;
    [SerializeField] AudioSource trash_dispose;
    [SerializeField] AudioSource trash_collect;

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
    }
    private void Start()
    {
        if (bg_clip != null)
            BG_Music(bg_clip);
    }
    public void BG_Music(AudioClip music)
    {
        bg_music.clip = music;
        bg_music.Play();
    }
    public void STOP_BG_Music()
    {
        bg_music.Stop();
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
    public void TrashDispose(AudioClip music)
    {
        trash_dispose.clip = music;
        trash_dispose.Play();
    }
    public void TrashCollect()
    {
        if (trash_collect != null)
        {
            trash_collect.Play();
        }
    }
}