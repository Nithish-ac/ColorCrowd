using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        IronSource.Agent.init("1d35f3f0d", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        Invoke("InitBanner", 3);
        Invoke("LoadFullScreen", 5);
    }

    void InitBanner()
    {
        IronSource.Agent.loadBanner(new IronSourceBannerSize(320, 50), IronSourceBannerPosition.BOTTOM);
    }
    public void LoadFullScreen()
    {
        IronSource.Agent.loadInterstitial();
    }
    public void ShowFullScreen()
    {
        IronSource.Agent.showInterstitial();
    }
}
