using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RunGamePlayManager : MonoBehaviour
{
    public static RunGamePlayManager instance;

    [HideInInspector] public List<ProductInLevel> productsCollected = new List<ProductInLevel>();

    private void Awake()
    {
        instance = this;
    }

    public void AddProductToList(productType productType)
    {
        //check if productType is available in productsInLevel

        for (int i = 0; i < productsCollected.Count; i++)
        {
            if (productsCollected[i].productType.CompareTo(productType) == 0)
            {
                productsCollected[i].countOfProducts++; break;
            }
        }

        //update UI of emojis 
        UIController.instance.SetEmojiListInGame(productsCollected);

    }

    public void CreateProductList(List<ProductInLevel> productInLevel)
    {

        for (int i = 0; i < productInLevel.Count; i++)
        {
            ProductInLevel newProduct = new ProductInLevel();
            newProduct.productType = productInLevel[i].productType;
            newProduct.countOfProducts = productInLevel[i].countOfProducts;
            productsCollected.Add(newProduct);
        }

        //reset counters of each product
        for (int i = 0; i < productsCollected.Count; i++)
        {
            productsCollected[i].countOfProducts = 0;
        }

    }
    public void CheckWinner()
    {

        LevelInfo.instance.CheckWin(productsCollected);
        UIController.instance.SetAllProductsInList(productsCollected, false);

    }

}
