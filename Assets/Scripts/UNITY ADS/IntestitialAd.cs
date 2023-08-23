using UnityEngine;
using UnityEngine.Advertisements;

public class IntestitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string AndroidAdUnitID;
    public string IOSAdUnitID;
    private string AdUnitID;


    private void Awake()
    {
#if UNITY_IOS
        AdUnitID = IOSAdUnitID;
#elif UNITY_ANDROID
        AdUnitID = AndroidAdUnitID;
#endif

    }

    void LoadAd()
    {
        Debug.Log("Load intestital");
        Advertisement.Load(AdUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("ad loaded");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("ad has error");

    }

    public void ShowAd()
    {
        Advertisement.Show(AdUnitID, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Completed");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("failure");

    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad start");
    }
}