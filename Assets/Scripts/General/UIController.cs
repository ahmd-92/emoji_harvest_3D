using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    [Header("General UI")]
    public GameObject commonPanel;
    public GameObject NoInternetPanel;
    public GameObject mainPanel;
    public GameObject lossPanel;
    public GameObject winPanel;
    public GameObject inGamePanel;
    public GameObject skinsPanel;
    public GameObject mySetupPanel;

    public List<TextMeshProUGUI> coinsTextList;
    public GameObject moneyUIPrefab;
    public Transform cashUITransfrom;
    public GameObject roomButton;
    public GameObject needMoreCoinsPrefab;
    public GameObject coinsGainedPrefab;
    public GameObject tryAgainInternetButton;
    public GameObject diffMenu;

    [Header("InGame Panel")]
    public GameObject TutorialText;
    public float timeToShowList;
    public GameObject StartListPanel;
    public GameObject inGameEmojisList;
    public TextMeshProUGUI timerPMRO;
    public List<ItemUIController> startItemsList;
    public List<ItemUIController> ingameItemList;

    [Header("Win Panel")]
    public GameObject incomeParent;
    public TextMeshProUGUI incomeTmpro;

    [Header("Skins Panel")]
    public GameObject gotoSkinsButton;
    public GameObject swipeSkinLeftButton;
    public GameObject swipeSkinRightButton;
    public GameObject buySkinButton;
    public GameObject selectSkinButton;
    public TextMeshProUGUI priceBuySkinButton;
    public GameObject currentSkinButton;

    [Header("Reward Buttons")]
    public List<GameObject> rewardButtonsList;

    [Header("Haptic & Sound")]
    public Image hapticButtonImage;
    public Sprite[] hapticSpriteArr = new Sprite[2];
    public Image soundButtonImage;
    public Sprite[] soundSpriteArr = new Sprite[2];

    private bool canClickAnyMainMenuButton = true;
    private bool canShowLevelProducts = false;
    private int currentLevel = 0;
    private int currentSkinPrice = 0;
    public static UIController instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = GamePreferences.GetLevel();
        //-------------GENERAL------------------------
        commonPanel.SetActive(true);
        mainPanel.SetActive(true);
        winPanel.SetActive(false);
        inGamePanel.SetActive(false);
        lossPanel.SetActive(false);
        skinsPanel.SetActive(false);
        NoInternetPanel.SetActive(false);
        mySetupPanel.SetActive(false);
        SetHapticImage();
        SetSoundImage();
        ToggleTutorialText(false);

        //Coins
        UpdateCoinText();

        StartListPanel.SetActive(false);
        timerPMRO.gameObject.SetActive(false);
        for (int i = 0; i < startItemsList.Count; i++)
        {
            startItemsList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < ingameItemList.Count; i++)
        {
            ingameItemList[i].gameObject.SetActive(false);
        }
        incomeParent.SetActive(false);
        roomButton.SetActive(currentLevel > 0);

        //reward buttons
        //ToggleRewardsButton(currentLevel > 1);
        gotoSkinsButton.SetActive(currentLevel > 1);


        OnClickTryAgainInternet();


    }
    private void ToggleRewardsButton(bool isEnable)
    {

        for (int i = 0; i < rewardButtonsList.Count; i++)
        {
            rewardButtonsList[i].SetActive(isEnable);

        }

    }

    #region MainMenu functions

    #region Internet Check
    public void OnClickTryAgainInternet()
    {
        StartCoroutine("CheckInternetCor");
    }
    IEnumerator CheckInternetCor()
    {
        //NoInternetPanel.SetActive(false);
        tryAgainInternetButton.SetActive(false);
        diffMenu.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        bool isInternetAvailable = GameManager.instance.CheckInternet();
        NoInternetPanel.SetActive(!isInternetAvailable);
        diffMenu.SetActive(isInternetAvailable);
        tryAgainInternetButton.SetActive(true);
    }
    #endregion


   

    #region EndGame
    public void EndGame(bool isWin, int incomePerEmoji = 0)
    {
        GameManager.instance.isGameOn = false;

        GamePreferences.AddGameCoins(incomePerEmoji);
        incomeTmpro.text = "+" + incomePerEmoji;
        incomeParent.SetActive(true);

        if (isWin)
        {
            StartCoroutine("ShowNextCoRoutine");
        }
        else
        {
            StartCoroutine("ShowResetCoRoutine");
        }
    }

    IEnumerator ShowNextCoRoutine()
    {
        PlayerManager.Instance.PlayWinFX();
        GamePreferences.SubLevelCountToLevelUp();
        GamePreferences.SetLevel(currentLevel + 1);
        if (!GamePreferences.IsTutorialLevelDone())
        {
            GamePreferences.TutorialLevelDone();
        }
        CameraManager.instance.ShakeCamera();
        yield return new WaitForSeconds(1);

        winPanel.SetActive(true);
        inGamePanel.SetActive(false);
        StartListPanel.SetActive(true);
        GameManager.instance.VibrateSucess();

        yield return new WaitForSeconds(3);

        StartNextLevel(true);

    }

    IEnumerator ShowResetCoRoutine()
    {

        yield return new WaitForSeconds(1);
        lossPanel.SetActive(true);
        inGamePanel.SetActive(false);
        StartListPanel.SetActive(true);
        GameManager.instance.VibrateFailure();

        yield return new WaitForSeconds(3);
        StartNextLevel(false);

    }

    public void StartNextLevel(bool isNext)
    {
        if (isNext)
        {
            GameManager.instance.NextLevel(0.1f);
        }
        else
        {

            GameManager.instance.RestartLevel(0.1f);
        }

    }
    #endregion

    public void UpdateCoinText()
    {
        string updatedCoinsText = GamePreferences.FormatNumberText(GamePreferences.GetGameCoins());
        for (int i = 0; i < coinsTextList.Count; i++)
        {
            coinsTextList[i].text = updatedCoinsText;
        }

    }


    public void ActivateMoneyUI(int value, Transform _transform)
    {

        Vector3 canvasPosition = Camera.main.WorldToScreenPoint(_transform.position);

        GameObject scoreMoney = Instantiate(moneyUIPrefab);
        scoreMoney.transform.SetParent(mainPanel.transform);
        scoreMoney.transform.position = canvasPosition;

        //  Vector3 target = UIController.Instance.coinIconTransfrom.position;
        scoreMoney.transform.SetParent(cashUITransfrom);
        scoreMoney.transform.localScale = Vector3.zero;
        scoreMoney.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            scoreMoney.transform.DOLocalMove(Vector3.zero, 1.15f).SetDelay(0.15f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                GamePreferences.AddGameCoins(value);
                scoreMoney.transform.DOScale(Vector3.zero, 0.075f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(scoreMoney.gameObject);
                });
            });
        });

    }
    #endregion
    public void SetAllProductsInList(List<ProductInLevel> productsInLevelList, bool atStartOfGame)
    {

        if (atStartOfGame)
        {
            //set products
            for (int i = 0; i < productsInLevelList.Count; i++)
            {
                //startItem
                startItemsList[i].gameObject.SetActive(true);
                startItemsList[i].emojiCountText.text = "x" + productsInLevelList[i].countOfProducts;
                Sprite newEmojiSprite = ProuctsDatabase.instance.GetProductSprite(productsInLevelList[i].productType);
                startItemsList[i].emojiImage.sprite = newEmojiSprite;

                //ingame item
                ingameItemList[i].gameObject.SetActive(true);
                ingameItemList[i].emojiCountText.text = "x" + 0;
                ingameItemList[i].emojiImage.sprite = newEmojiSprite;



            }
            canShowLevelProducts = true;

        }
        else
        {
            //reset UI first
            foreach (ItemUIController GO in startItemsList)
            {
                GO.gameObject.SetActive(false);

            }

            //get products
            for (int i = 0; i < productsInLevelList.Count; i++)
            {
                startItemsList[i].gameObject.SetActive(true);



                int productCollected = productsInLevelList[i].countOfProducts;
                int productInLevel = LevelInfo.instance.productsInLevel[i].countOfProducts;

                startItemsList[i].emojiCountText.text = productCollected + " / " + productInLevel;

                if (productCollected == productInLevel)
                {
                    startItemsList[i].emojiCountText.color = Color.green;
                }
                else
                {
                    startItemsList[i].emojiCountText.color = Color.red;
                }


            }
        }

    }
    public void SetEmojiListInGame(List<ProductInLevel> productsInLevelList)
    {

        for (int i = 0; i < productsInLevelList.Count; i++)
        {
            ingameItemList[i].gameObject.SetActive(true);
            ingameItemList[i].emojiCountText.text = "X" + productsInLevelList[i].countOfProducts;


            //set emoji sprite
            ingameItemList[i].emojiImage.sprite = ProuctsDatabase.instance.GetProductSprite(productsInLevelList[i].productType);

        }
    }
    public void OnClickLevelDiffButton(int levelDiffIndex)
    {
        if (!canClickAnyMainMenuButton) return;
        canClickAnyMainMenuButton = false; 
        GameManager.instance.SwitchGamePlay(GameStatus.RunGame, (LevelDifficulty)levelDiffIndex);
        //ToggleSetupPanel(false);
        StartCoroutine("ShowLevelProductListCor");
    }
   

   
    IEnumerator ShowLevelProductListCor()
    {
        //wait until we got ok from levelInfo
        yield return new WaitUntil(() => canShowLevelProducts);

        mainPanel.SetActive(false);
        inGamePanel.SetActive(true);

        //show products needed
        inGameEmojisList.SetActive(false);
        StartListPanel.SetActive(true);


        //timer
        float angle = timeToShowList;
        DOTween.To(() => angle, x => angle = x, 0, timeToShowList)
            .OnUpdate(() =>
            {
                timerPMRO.text = "" + (int)(angle + 1);
            }).OnComplete(() =>
            {

                timerPMRO.gameObject.SetActive(false);

            });

        timerPMRO.gameObject.SetActive(true);

        yield return new WaitForSeconds(timeToShowList);

        inGameEmojisList.SetActive(true);
        StartListPanel.SetActive(false);
        ToggleTutorialText(true);
        StartCoroutine("StartTheGameCoRoutine");

    }
    IEnumerator StartTheGameCoRoutine()
    {

        yield return new WaitForSeconds(0.1f);
        PlayerManager.Instance.PlayerStartRun();
        GameManager.instance.isGameOn = true;

    }
    public void ToggleTutorialText(bool isEnable)
    {

        TutorialText.SetActive(isEnable);

        if (isEnable == true)
        {
            StartCoroutine("TutorialTextCor");
        }
    }

    IEnumerator TutorialTextCor()
    {
        Vector3 initialScale = TutorialText.transform.localScale;
        TutorialText.transform.DOScale(initialScale * 1.4f, 0.25f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(2);
        TutorialText.transform.DOKill();
        TutorialText.SetActive(false);
        TutorialText.transform.localScale = initialScale;

    }
    public void OnClickBackToMainMenuFromMySetup()
    {
        GameManager.instance.SwitchGamePlay(GameStatus.MainMenu);
        mySetupPanel.SetActive(false);
        mainPanel.SetActive(true);
       
        canClickAnyMainMenuButton = true;
    }

    public void OnClickMySetupButton()
    {
        if (!canClickAnyMainMenuButton) return;
        canClickAnyMainMenuButton = false;

        GameManager.instance.SwitchGamePlay(GameStatus.MyPcGame);
        mainPanel.SetActive(false);
        mySetupPanel.SetActive(true);

    }


    public void CreateNewNeedMoreCoins()
    {
        GameObject newUI = Instantiate(needMoreCoinsPrefab, commonPanel.transform);
        Destroy(newUI, 2f);

    }
    public void CreateNewCoinsGained()
    {
        GameObject newUI = Instantiate(coinsGainedPrefab, commonPanel.transform);
        Destroy(newUI, 2f);

    }
   
    #region Player Skins

    public void OnClickGoToSkins()
    {
        if (!canClickAnyMainMenuButton) return;
        canClickAnyMainMenuButton = false;

        skinsPanel.SetActive(true);
        mainPanel.SetActive(false);
        CheckSwipeLeftRightButtons();
        RefreshBuySelectButtons();
        GameManager.instance.SwitchGamePlay(GameStatus.SkinsGame);
       

    }

    public void OnClickBackFromSkins()
    {
        Debug.Log("OnClickBackFromSkins");
        skinsPanel.SetActive(false);
        mainPanel.SetActive(true);
        GameManager.instance.SwitchGamePlay(GameStatus.MainMenu);
        canClickAnyMainMenuButton = true;
    }

    public void OnClickSwipeSkinLeftButton()
    {

        PlayerManager.Instance.ChangeSkin(-1);
        StartCoroutine("ToggleSwipeButtonsAfterClickCor");
    }


    public void OnClickSwipeSkinRightButton()
    {

        PlayerManager.Instance.ChangeSkin(1);

        StartCoroutine("ToggleSwipeButtonsAfterClickCor");

    }

    public void OnClickSelectSkin()
    {
        PlayerManager.Instance.SetCurrentSkinIndex(PlayerManager.Instance.currentSkinId);
        RefreshBuySelectButtons();
    }

    public void OnClickUnlockSkin()
    {
        //check price 
        if (PlayerManager.Instance.CanBuySkin())
        {
            GamePreferences.SubtractGameCoins(currentSkinPrice);
            PlayerManager.Instance.UnlockSkin();
            RefreshBuySelectButtons();
            PlayerManager.Instance.PlayNewSkinFX();

            //crazygames
            // CrazygamesManager.instance.HappyTime();
        }
        else
        {
            CreateNewNeedMoreCoins();
        }

    }
    public void ToggleSwipeSkinLefttButton(bool isEnable)
    {
        swipeSkinLeftButton.SetActive(isEnable);
    }
    public void ToggleSwipeSkinRightButton(bool isEnable)
    {
        swipeSkinRightButton.SetActive(isEnable);
    }

    IEnumerator ToggleSwipeButtonsAfterClickCor()

    {

        swipeSkinLeftButton.SetActive(false);
        swipeSkinRightButton.SetActive(false);
        buySkinButton.SetActive(false);
        selectSkinButton.SetActive(false);
        currentSkinButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        CheckSwipeLeftRightButtons();
        RefreshBuySelectButtons();
    }
    public void RefreshBuySelectButtons()
    {
        int currentSkinIndex = PlayerManager.Instance.currentSkinId;
        buySkinButton.SetActive(false);
        selectSkinButton.SetActive(false);
        currentSkinButton.SetActive(false);
        //check if current skin already unlock
        if (PlayerManager.Instance.isSkinUnlocked(currentSkinIndex))
        {
            if (currentSkinIndex == PlayerManager.Instance.GetCurrentSkinIndex())
            {

                currentSkinButton.SetActive(true);
            }
            else
            {
                selectSkinButton.SetActive(true);
            }

        }
        else
        {
            buySkinButton.SetActive(true);
            //get price and set it to text
            currentSkinPrice = PlayerManager.Instance.skinsList[currentSkinIndex].price;
            priceBuySkinButton.text = GamePreferences.FormatNumberText(currentSkinPrice);
        }

    }

    void CheckSwipeLeftRightButtons()
    {
        int currentSkinId = PlayerManager.Instance.currentSkinId;
        ToggleSwipeSkinLefttButton(currentSkinId != 0);
        ToggleSwipeSkinRightButton(currentSkinId != PlayerManager.Instance.GetMaxSkinsCount() - 1);
    }
    #endregion
    #region Haptic && Sounds

    public void OnClickHapticButton()
    {

        GamePreferences.SetVibration(!GamePreferences.IsVibrationOn());
        SetHapticImage();
    }
    public void SetHapticImage()
    {

        if (GamePreferences.IsVibrationOn())
        {
            hapticButtonImage.sprite = hapticSpriteArr[0];
        }
        else
        {
            hapticButtonImage.sprite = hapticSpriteArr[1];

        }
    }

    public void OnClickSoundButton()
    {

        GamePreferences.SetSounds(!GamePreferences.IsSoundOn());
        SetSoundImage();
    }
    public void SetSoundImage()
    {

        if (GamePreferences.IsSoundOn())
        {
            soundButtonImage.sprite = soundSpriteArr[0];
        }
        else
        {
            soundButtonImage.sprite = soundSpriteArr[1];

        }
    }

    #endregion
}
