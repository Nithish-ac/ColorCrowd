using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorCrowd
{
    public class IronSourceAds : MonoBehaviour
    {
        public static IronSourceAds Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            IronSource.Agent.init("1d35f3f0d", IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
            Invoke(nameof(InitBanner), 3);
            Invoke("LoadFullScreen", 5);
        }

        void InitBanner()
        {
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
            IronSource.Agent.displayBanner();
        }
        public void ShowFullScreen()
        {
            IronSource.Agent.loadInterstitial();
            Invoke(nameof(EnableAd),1f);
        }
        public void EnableAd()
        {
            IronSource.Agent.showInterstitial();
        }
    }
}
