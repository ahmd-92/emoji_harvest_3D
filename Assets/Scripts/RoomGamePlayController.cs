using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public enum ItemsName { Table, Monitor, KeyboardMouse, Desktop, Chair }



[System.Serializable]
public class ItemInRoom
{
    public ItemsName itemName;
    public Transform itemParentTrans;
    public List<GameObject> itemList;
    public Button updateItemButton;
    public TextMeshProUGUI priceTmpro;
    public GameObject coinIcon;
    [HideInInspector] public GameObject currentItemPrefab;
}


public class RoomGamePlayController : MonoBehaviour
{

    public static RoomGamePlayController instance;

    private int RoomID = 0;
    [Header("Database")]
    public List<ItemInRoom> itemsInRoomList;

    [Header("UI")]
    public GameObject updateRoomParent;
    public TextMeshProUGUI toggleUpdateTmpro;

    [Header("Components")]
    public Transform SetupRoomTrans;
    public Transform camParentTrans;


    private bool isUpdatesHidden = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ToggleSetupRoomComponents(false);
    }

    public void InitSetupRoom()
    {


        isUpdatesHidden = false;
        ToggleUpdateButton(isUpdatesHidden);

        //load all available items 
        for (int i = 0; i < itemsInRoomList.Count; i++)
        {
            int itemLevel = GetItemLevel(itemsInRoomList[i].itemName, RoomID);

            itemsInRoomList[i].currentItemPrefab = Instantiate(itemsInRoomList[i].itemList[itemLevel], itemsInRoomList[i].itemParentTrans);
            itemsInRoomList[i].currentItemPrefab.transform.localPosition = Vector3.zero;

            // update UI
            if (itemLevel <= 3)
            {

                itemsInRoomList[i].priceTmpro.text = GamePreferences.FormatNumberText(GetItemPrice(itemsInRoomList[i].itemName, RoomID));
            }
            else//max
            {
                itemsInRoomList[i].updateItemButton.enabled = false;
                itemsInRoomList[i].coinIcon.SetActive(false);
                itemsInRoomList[i].priceTmpro.text = " MAX";
            }

        }

        ToggleSetupRoomComponents(true);

    }
    public void ToggleSetupRoomComponents(bool isEnable)
    {

        SetupRoomTrans.gameObject.SetActive(isEnable);

    }
    public void ToggleUpdateButton(bool isHide)
    {
        if (isHide)
        {
            toggleUpdateTmpro.text = "Show";
            ToggleUpdatePanel(false);

        }
        else
        {
            toggleUpdateTmpro.text = "Hide";
            ToggleUpdatePanel(true);
        }
    }

    public void OnClickToggleRoomUpdates()
    {
        isUpdatesHidden = !isUpdatesHidden;
        ToggleUpdateButton(isUpdatesHidden);

    }
    public void OnClickUpdateItemButton(int itemIndex)
    {

        ItemsName itemName = (ItemsName)itemIndex;
        int itemPrice = GetItemPrice(itemName, RoomID);

        print(itemName + " button clicked");

        //check if we have enough coins update it, if no coins create UI : need more coins
        if (GamePreferences.GetGameCoins() >= itemPrice)//we have enough coins
        {
            AddItemLevel(itemName, RoomID);
            GamePreferences.SubtractGameCoins(itemPrice);
            StartCoroutine(UpdateItemCor(itemName));
            //   GameManager.instance.VibrateMedium();
        }
        else//No coins
        {
            //Need More Coins text
            UIController.instance.CreateNewNeedMoreCoins();
        }

    }



    IEnumerator UpdateItemCor(ItemsName itemName)
    {
        ToggleUpdatePanel(false);
        yield return new WaitForSeconds(0);
        for (int i = 0; i < itemsInRoomList.Count; i++)
        {
            if (itemsInRoomList[i].itemName.CompareTo(itemName) == 0)
            {
                GameObject oldItem = itemsInRoomList[i].currentItemPrefab;
                oldItem.transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.Linear);
                Destroy(oldItem, 0.3f);

                int itemLevel = GetItemLevel(itemsInRoomList[i].itemName, RoomID);

                yield return new WaitForSeconds(0.15f);
                itemsInRoomList[i].currentItemPrefab = Instantiate(itemsInRoomList[i].itemList[itemLevel], itemsInRoomList[i].itemParentTrans);
                itemsInRoomList[i].currentItemPrefab.transform.localPosition = Vector3.zero;
                Vector3 intialScale = itemsInRoomList[i].currentItemPrefab.transform.localScale;
                itemsInRoomList[i].currentItemPrefab.transform.localScale = Vector3.zero;
                itemsInRoomList[i].currentItemPrefab.transform.DOScale(intialScale, 0.25f).SetEase(Ease.OutBounce);

                //  CreateNewItemFX();

                // update UI
                if (itemLevel <= 3)
                {

                    itemsInRoomList[i].priceTmpro.text = GamePreferences.FormatNumberText(GetItemPrice(itemsInRoomList[i].itemName, RoomID));
                }
                else//max
                {
                    itemsInRoomList[i].updateItemButton.enabled = false;
                    itemsInRoomList[i].coinIcon.SetActive(false);
                    itemsInRoomList[i].priceTmpro.text = "MAX";
                }


                yield return new WaitForSeconds(1);
                ToggleUpdatePanel(true);


            }

        }

    }

    public void ToggleUpdatePanel(bool isEnable)
    {

        updateRoomParent.SetActive(isEnable);

    }




    #region Save System
    //ROOM SAVE SYSTEM

    //TABLE
    //get level item 
    public static int GetItemLevel(ItemsName itemName, int roomid)
    {
        // room id, 
        return PlayerPrefs.GetInt(itemName + "_level_room_id_" + roomid, 0);
    }
    //set level item
    public static void AddItemLevel(ItemsName itemName, int roomid)
    {
        PlayerPrefs.SetInt(itemName + "_level_room_id_" + roomid, GetItemLevel(itemName, roomid) + 1);
    }
    //get price for item
    public static int GetItemPrice(ItemsName itemName, int roomid)
    {
        int itemLevel = GetItemLevel(itemName, roomid);
        //calculate the price: (level x addValue) + initialPrice

        //int calculatedPrice = (itemLevel * 100) + (50 * (itemLevel+2));
        //another method: add the price manually in a table depend on 
        int manualPrice = 0;
        switch (itemLevel)//check how many levels each item 
        {
            case 0:
                manualPrice = 150;
                break;
            case 1:
                manualPrice = 650;
                break;
            case 2:
                manualPrice = 1500;
                break;
            case 3:
                manualPrice = 6000;
                break;
            default:
                print("Table level is not found");
                break;
        }

        return manualPrice;
    }
    #endregion
}
