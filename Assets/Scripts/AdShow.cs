using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking;

public class AdShow : MonoBehaviour
{
    
    private InterstitialAd interstitialAd;
    private BannerView bannerView;
    public GameObject menuBuy;
    
    
    public void Awake()
    {
        if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
            MobileAds.Initialize(InitializationStatus => { });
    }
    public void Start()
    {

        if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
           MobileAds.Initialize(InitializationStatus => { });


       if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
          this.RequestBanner();

    }

    public void Update()
    {
        if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
        {
            if (Application.internetReachability == 0) //Not reachable at all
            {
                menuBuy.SetActive(true);
            }
            else
            {
                menuBuy.SetActive(false);
            }
        }   
    }

    private void OnEnable()
    {
        string interad = "ca-app-pub-2178870922346897/3261163639";

        if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
        {
            interstitialAd = new InterstitialAd(interad);
            AdRequest adRequestInt = new AdRequest.Builder().Build();
            interstitialAd.LoadAd(adRequestInt);
        }
    }
    public void ShowVideo()
    {
        if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }
        }
    }
        private void RequestBanner()
    {
        if (!(PlayerPrefs.HasKey("removeAds") && PlayerPrefs.GetInt("removeAds") == 1))
        {
            string adUnitId = "ca-app-pub-2178870922346897/4574245304";
            AdRequest adRequest = new AdRequest.Builder().Build();


            bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

            bannerView.LoadAd(adRequest);
        }
    }
}
