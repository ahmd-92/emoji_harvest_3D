using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;

[System.Serializable]
public class Skin
{
    public GameObject skinprefab;
    public int price;
}

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance;


    [Header("Player")]
    public RunGamePlayManager runGamePlayManager;

    [Header("Player")]
    public Transform playerShapeTrans;
    public Transform emojisParentTrans;
    public Transform mainCameraTrans;
    public Transform camParentTrans;
    public Animator anim;
    public GameObject collectFxPrefab;
    public GameObject plusIncomePrefab;


    [Header("FX")]
    public ParticleSystem winFX;
    public ParticleSystem windFX;
    public ParticleSystem newSkinFX;



    [Header("Player Skins")]
    public List<Skin> skinsList;


    //private:
    [HideInInspector] public int currentSkinId = 0;
    private PlayerControls playerControls;
    private List<GameObject> collectedEmojisList = new List<GameObject>();//this is just visually

    private void Awake()
    {
        Instance = this;
    }
    #region Mono
    // Start is called before the first frame update
    void Start()
    {

        playerControls = GetComponent<PlayerControls>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("product"))
        {

            other.gameObject.GetComponent<Collider>().enabled = false;
            runGamePlayManager.AddProductToList(other.gameObject.GetComponent<Product>().thisProductType);
            collectedEmojisList.Add(other.gameObject);
            other.gameObject.SetActive(false);
            other.transform.parent = emojisParentTrans;
            other.transform.localPosition = Vector3.zero;
            Instantiate(collectFxPrefab, new Vector3(playerShapeTrans.position.x, playerShapeTrans.position.y + 1f, playerShapeTrans.position.z), Quaternion.identity);
            SoundManager.instance.PlaySoundFX(SoundFXType.collect);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {

            StartCoroutine("StopPlayerMoevementCor");
        }
    }
    #endregion
    public void InitPlayer()
    {
        currentSkinId = GetCurrentSkinIndex();
        ChangeSkin();

    }
    public void PlayerStartRun()
    {
        //intialize 
        //palyer controls
        playerControls.StartMove(this);

        //animation
        TriggerAnimator("Run");

        //fx
        if (windFX != null) windFX.Play();

    }

    IEnumerator StopPlayerMoevementCor()
    {
        playerControls.startMove = false;
        Vector3.Lerp(playerShapeTrans.localEulerAngles, Vector3.zero, 0.25f);
        //animation
        TriggerAnimator("Idle");
        if (windFX != null) windFX.Stop();

        int EmojiCount = collectedEmojisList.Count;
        float timeBetweenEmoji = 1.5f / (float)EmojiCount;
        //start drop emojis in finish line
        // print("collectedEmojisList Count :" + collectedEmojisList.Count);
        Transform dropArea = LevelInfo.instance.emojiDropAreaTransform;
        int incomePerEmoji = LevelInfo.instance.incomeFromEmoji;
        for (int i = EmojiCount - 1; i >= 0; i--)
        {
            GameObject currentEmoji = collectedEmojisList[i];
            currentEmoji.transform.parent = dropArea;
            currentEmoji.transform.localScale = Vector3.zero;
            currentEmoji.SetActive(true);
            StartCoroutine(DeactivateEmojiCor(currentEmoji, incomePerEmoji));
            currentEmoji.transform.DOScale(Vector3.one, 1).SetEase(Ease.OutCubic);
            currentEmoji.transform.DOLocalJump(Vector3.zero, 2, 1, 1).SetEase(Ease.Linear);


            yield return new WaitForSeconds(timeBetweenEmoji);

        }

        yield return new WaitForSeconds(1);
        runGamePlayManager.CheckWinner();

    }

    IEnumerator DeactivateEmojiCor(GameObject go, int incomePerEmoji)
    {
        yield return new WaitForSeconds(1f);

        go.SetActive(false);
        GameObject newPlusFX = Instantiate(plusIncomePrefab.gameObject);
        newPlusFX.transform.position = go.transform.position;
        newPlusFX.GetComponent<PlusIncome>().EditIncomeText(incomePerEmoji);
    }


    void TriggerAnimator(string animName)
    {
        if (anim != null)
        {
            anim.SetTrigger(animName);
        }
    }

    public void PlayWinFX()
    {

        if (winFX != null)
        {
            winFX.Play();
        }

    }

  

    #region Player Skin
    public int GetMaxSkinsCount()
    {

        return skinsList.Count;

    }

    public void ChangeSkin(int newSkinID = 0)
    {
        StartCoroutine(ChangeSkinCor(newSkinID));

    }

    IEnumerator ChangeSkinCor(int newSkinID)
    {

        currentSkinId += newSkinID;

        float timeToWait = 0;
        //destroy old
        if (playerShapeTrans.childCount > 0)
        {
            GameObject oldSkin = playerShapeTrans.GetChild(0).gameObject;
            oldSkin.transform.DOScale(Vector3.zero, 0.25f);
            Destroy(oldSkin, 0.3f);
            timeToWait = 0.25f;
        }

        yield return new WaitForSeconds(timeToWait);
        //instantiate new
        GameObject newSkin = Instantiate(skinsList[currentSkinId].skinprefab, playerShapeTrans);
        Vector3 initialScale = newSkin.transform.localScale;
        newSkin.transform.localPosition = Vector3.zero;
        newSkin.transform.localRotation = Quaternion.identity;
        newSkin.transform.localScale = Vector3.zero;
        newSkin.transform.DOScale(initialScale, 0.5f).SetEase(Ease.OutBounce);

        //get anima
        anim = newSkin.GetComponent<Animator>();
    }
    public bool CanBuySkin()
    {
        bool result = false;
        if (GamePreferences.GetGameCoins() >= skinsList[currentSkinId].price)
        {
            result = true;
        }
        return result;
    }

    public void PlayNewSkinFX()
    {

        if (newSkinFX != null)
        {
            newSkinFX.Play();
        }
    }

    #region  Skin Save
    public static string currentSavedSkinIndex = "currentSavedSkinIndex";
    public int GetCurrentSkinIndex()
    {
        // room id, 
        return PlayerPrefs.GetInt(currentSavedSkinIndex, 0);
    }
    //set level item
    public void SetCurrentSkinIndex(int index)
    {
        PlayerPrefs.SetInt(currentSavedSkinIndex, index);
    }

    public void UnlockSkin()
    {

        PlayerPrefs.SetInt("skin_" + currentSkinId + "_isunlocked_", 1);

    }
    public bool isSkinUnlocked(int skinIndex)
    {
        bool result = false;
        if (skinIndex == 0)
        {
            result = true;
        }
        else
        {
            result = PlayerPrefs.GetInt("skin_" + skinIndex + "_isunlocked_", 0) > 0;
        }


        return result;
    }
    #endregion


    #endregion
}




