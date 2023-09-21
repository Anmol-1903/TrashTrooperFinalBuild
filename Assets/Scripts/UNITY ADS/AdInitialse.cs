using UnityEngine;
using UnityEngine.Advertisements;

public class AdInitialse : MonoBehaviour,IUnityAdsInitializationListener
{
    public string AndroidAdId;
    public string IOSAdId;
    private bool isTestingMode = false;
    private string gameId;

    private void Start()
    {
        isTestingMode = false;
        PlayAd();
    }
    private void PlayAd()
    {
#if UNITY_IOS
       gameId = IOSAdId
#elif UNITY_ANDROID
        gameId = AndroidAdId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, isTestingMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ad complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("error");
    }
}
