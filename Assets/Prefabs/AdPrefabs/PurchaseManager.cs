using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour,IStoreListener {

    private IStoreController storecontroller;
    private IExtensionProvider extensionsprovider;
    private bool purchaseInitialized, internetConnected;

    public void Start()
    {



        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("nombre_variable", ProductType.Consumable, new IDs
        {
            {"pack_1", GooglePlay.Name },
            {"pack_2", GooglePlay.Name },
            {"pack_3", GooglePlay.Name },
            {"pack_4", GooglePlay.Name }

        });

        UnityPurchasing.Initialize(this, builder);

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Y'A PAS INTERNET");
            internetConnected = false;
        }

        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {

            Debug.Log("Y'A INTERNET");
            internetConnected = true;
        }
    }

    public void BuyPack_1()
    {
        BuyProductID("pack_1");
    }

    public void BuyPack_2()
    {
        BuyProductID("pack_2");
    }

    public void BuyPack_3()
    {
        BuyProductID("pack_3");
    }

    public void BuyPack_4()
    {
        BuyProductID("pack_4");
    }

    private void BuyProductID(string productID)
    {
       if(internetConnected==true && purchaseInitialized)
        {
            Product product = storecontroller.products.WithID(productID);

            if(product !=null && product.availableToPurchase)
            {
                Debug.Log(string.Format("ACHAT DU PRODUIT : ", product.definition.id));
                storecontroller.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("FAIL, PRODUIT NON EXISTANT OU INDISPONIBLE");
            }
        }
        else
        {
            Debug.Log("NOT CONNECTED OR PRODUCT NOT LOADED.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.storecontroller = controller;
        this.extensionsprovider = extensions;
        purchaseInitialized = true;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Fail de l'initialisation. Très très content, j'adore : "+error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        
        if (string.Equals(args.purchasedProduct.definition.id, "pack_1", StringComparison.Ordinal))
        {
            Debug.Log("pack_1 acheté, insérer la récompense voulue");
        }

        else if (string.Equals(args.purchasedProduct.definition.id, "pack_2", StringComparison.Ordinal))
        {
            Debug.Log("pack_2 acheté, insérer la récompense appropriée");
        }

        else if (string.Equals(args.purchasedProduct.definition.id, "pack_3", StringComparison.Ordinal))
        {
            Debug.Log("pack_3 acheté, insérer la récompense souhaitée");
        }

        else if (string.Equals(args.purchasedProduct.definition.id, "pack_4", StringComparison.Ordinal))
        {
            Debug.Log("pack_4 acheté, insérer la récompense qui l'accompagne");
        }

        else
        {
            Debug.Log(string.Format("PRODUIT ACHETÉ NON IDENTIFIÉ : ", args.purchasedProduct.definition.id));
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log(string.Format("ACHAT ÉCHOUÉ. PRODUIT & RAISON DE L'ÉCHEC :", i.definition.storeSpecificId,p));
    }

 
}
