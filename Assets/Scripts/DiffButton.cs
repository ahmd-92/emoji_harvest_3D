using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DiffButton : MonoBehaviour
{
    public LevelDifficulty buttonDiff;
    public Sprite lockedSprite;
    public Sprite unlockSprite;
    public Image buttonImage;
    public Button button;
    public TextMeshProUGUI diffText;
    public TextMeshProUGUI completeText;
    public GameObject lockImage;

    // Start is called before the first frame update
    void Start()
    {

        //check current player difficulty level 
        LevelDifficulty currenLevel = (LevelDifficulty)GamePreferences.GetCurrentLevelDifficulty();
       // print("currenLevel : " + currenLevel);
        switch (currenLevel)
        {
            case LevelDifficulty.Easy:
                switch (buttonDiff)
                {
                    case LevelDifficulty.Easy:
                        diffText.text = "Easy";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Mid:
                        diffText.text = "Medium";
                        button.enabled = false;
                        completeText.gameObject.SetActive(true);
                        completeText.text = "Complete " + GamePreferences.GetLevelCountToLevelUp() + " Easy Levels to Unlock";
                        lockImage.SetActive(true);
                        break;
                    case LevelDifficulty.Hard:
                        diffText.text = "Hard";
                        button.enabled = false;
                        completeText.gameObject.SetActive(false);
                        lockImage.SetActive(true);
                        break;
                    case LevelDifficulty.Pro:
                        diffText.text = "Expert";
                        button.enabled = false;
                        completeText.gameObject.SetActive(false);
                        lockImage.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case LevelDifficulty.Mid:
                switch (buttonDiff)
                {
                    case LevelDifficulty.Easy:
                        diffText.text = "Easy";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Mid:
                        diffText.text = "Medium";
                        button.enabled = true;
                        buttonImage.sprite = unlockSprite;
                        completeText.gameObject.SetActive(false);
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Hard:
                        diffText.text = "Hard";
                        button.enabled = false;
                        completeText.gameObject.SetActive(true);
                        completeText.text = "Complete " + GamePreferences.GetLevelCountToLevelUp() + " Midium Levels to Unlock";
                        lockImage.SetActive(true);
                        break;
                    case LevelDifficulty.Pro:
                        diffText.text = "Expert";
                        button.enabled = false;
                        completeText.gameObject.SetActive(false);
                        lockImage.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case LevelDifficulty.Hard:
                switch (buttonDiff)
                {
                    case LevelDifficulty.Easy:
                        diffText.text = "Easy";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Mid:
                        diffText.text = "Medium";
                        button.enabled = true;
                        buttonImage.sprite = unlockSprite;
                        completeText.gameObject.SetActive(false);
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Hard:
                        diffText.text = "Hard";
                        button.enabled = true;
                        buttonImage.sprite = unlockSprite;
                        completeText.gameObject.SetActive(false);
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Pro:
                        diffText.text = "Expert";
                        button.enabled = false;
                        completeText.gameObject.SetActive(true);
                        completeText.text = "Complete " + GamePreferences.GetLevelCountToLevelUp() + " Hard Levels to Unlock";
                        lockImage.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case LevelDifficulty.Pro:
                switch (buttonDiff)
                {
                    case LevelDifficulty.Easy:
                        diffText.text = "Easy";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Mid:
                        diffText.text = "Medium";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Hard:
                        diffText.text = "Hard";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    case LevelDifficulty.Pro:
                        diffText.text = "Expert";
                        button.enabled = true;
                        completeText.gameObject.SetActive(false);
                        buttonImage.sprite = unlockSprite;
                        lockImage.SetActive(false);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

}
