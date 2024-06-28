using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ProductElement
{
    public Sprite emojiSprite;
    public GameObject prefabEmojiInLevel;
    public productType productType;
   
}


public class ProuctsDatabase : MonoBehaviour
{
    public static ProuctsDatabase instance;
    public List<ProductElement> products;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

  /*  public GameObject GetProductShape(productType type)
    {

        GameObject result = null;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].productType.CompareTo(type) == 0)
            {
                result = products[i].productShape; break;   
            }
        }


        return result;
    }*/

    public Sprite GetProductSprite(productType type)
    {

        Sprite result = null;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].productType.CompareTo(type) == 0)
            {
                result = products[i].emojiSprite; break;
            }
        }


        return result;
    }

    public GameObject GetEmojiPrefab(productType type)
    {

        GameObject obj = null;

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].productType.CompareTo(type) == 0)
            {
                obj = products[i].prefabEmojiInLevel; break;
            }
        }


        return obj;
    }
}
