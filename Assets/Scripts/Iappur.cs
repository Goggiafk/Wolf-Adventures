using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
public class Iappur : MonoBehaviour, IStoreListener
{


    public static Iappur instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products
    private string removeAds = "remove_ads";


    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(removeAds, ProductType.NonConsumable);


        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyRemoveAds()
    {
        BuyProductID(removeAds);
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods




    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchace: PASS product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("removeAds", 1);
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }

    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchacing product asychronously: '{0}'", product.definition.id));
                changeScreenToPurchace();
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                changeScreenToPurchace();

            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
            changeScreenToPurchace();

        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    void changeScreenToPurchace()
    {
        
    }
}