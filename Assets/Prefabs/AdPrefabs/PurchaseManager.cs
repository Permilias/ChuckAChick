using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseManager : IStoreListener {

    private IStoreController storecontroller;
    private IExtensionProvider extensionsprovider;

    public PurchaseManager()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("nombre_variable", ProductType.Consumable, new IDs
        {
            {"nombre_variable_google", GooglePlay.Name }

        });

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.storecontroller = controller;
        this.extensionsprovider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Fail de l'achat. Très très content, j'adore");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
        
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        
    }

 
}
