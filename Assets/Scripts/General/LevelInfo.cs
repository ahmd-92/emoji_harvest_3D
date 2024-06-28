
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductInLevel
{
    public int countOfProducts;
    public productType productType;
}
public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;

    [Header("Auto Generate Level")]
    public List<PlatformController> platformControllerList;
    public Transform emojisParentTransform;

    [Header("General")]
    public int incomeFromLevel;
    public Transform emojiDropAreaTransform;
    public List<ProductInLevel> productsInLevel = new List<ProductInLevel>();
    public int incomeFromEmoji = 5;

    private LevelDifficulty levelDifficulty = LevelDifficulty.Easy;
    [HideInInspector] public int itemsToFinishLevel;

    private void Awake()
    {
        instance = this;
    }


    public void StartLevel(LevelDifficulty levelOfDif)
    {
        //Debug.Log("StartLevel, level: " + GamePreferences.GetLevel());
        levelDifficulty = levelOfDif;
        if (GamePreferences.GetLevel() > 0)//if tutorial level (0) already we have level
        {
            CreateLevel();
            GenerateLevel();
        }
        RunGamePlayManager.instance.CreateProductList(productsInLevel);
        UIController.instance.SetAllProductsInList(productsInLevel, true);
        int number = GetNumberOfAllProductInLevel();

    }

    void CreateLevel()
    {
        //Debug.Log("CreateLevel");
        //depen on the difficulty  we build level
        int emojisMin = 5;
        int emojisMax = 11;

        switch (levelDifficulty)
        {
            case LevelDifficulty.Easy:

                //easy : 1 emoji per level
                if (productsInLevel.Count > 0)
                {

                    List<int> randomResult = new List<int>();

                    int randomChoiceSplitNumber = Random.Range(0, 3);

                    if (randomChoiceSplitNumber == 1)
                    {
                        randomResult = RandomSplitV2(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }
                    else
                    {
                        randomResult = RandomSplit(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }

                    for (int i = 0; i < productsInLevel.Count; i++)
                    {
                        productsInLevel[i].countOfProducts = randomResult[i];
                    }

                }

                break;
            case LevelDifficulty.Mid:

                //mid : 2 emojis
                emojisMin = 10;
                emojisMax = 18;

                if (productsInLevel.Count > 0)
                {

                    List<int> randomResult = new List<int>();

                    int randomChoiceSplitNumber = Random.Range(0, 3);

                    if (randomChoiceSplitNumber == 1)
                    {
                        randomResult = RandomSplitV2(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }
                    else
                    {
                        randomResult = RandomSplit(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }

                    for (int i = 0; i < productsInLevel.Count; i++)
                    {
                        productsInLevel[i].countOfProducts = randomResult[i];
                    }


                }

                break;
            case LevelDifficulty.Hard:

                //hard: 3 or 4 items per level
                emojisMin = 14;
                emojisMax = 22;

                if (productsInLevel.Count > 0)
                {

                    List<int> randomResult = new List<int>();

                    int randomChoiceSplitNumber = Random.Range(0, 3);

                    if (randomChoiceSplitNumber == 1)
                    {
                        randomResult = RandomSplitV2(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }
                    else
                    {
                        randomResult = RandomSplit(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }

                    for (int i = 0; i < productsInLevel.Count; i++)
                    {
                        productsInLevel[i].countOfProducts = randomResult[i];
                    }


                }
                break;

            case LevelDifficulty.Pro:

                //Pro: 5 items per level
                emojisMin = 12;
                emojisMax = 26;

                if (productsInLevel.Count > 0)
                {

                    List<int> randomResult = new List<int>();

                    int randomChoiceSplitNumber = Random.Range(0, 3);

                    if (randomChoiceSplitNumber == 1)
                    {
                        randomResult = RandomSplitV2(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }
                    else
                    {
                        randomResult = RandomSplit(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }

                    for (int i = 0; i < productsInLevel.Count; i++)
                    {
                        productsInLevel[i].countOfProducts = randomResult[i];
                    }


                }

                break;

            default:
                //act like easy level

                if (productsInLevel.Count > 0)
                {

                    List<int> randomResult = new List<int>();

                    int randomChoiceSplitNumber = Random.Range(0, 3);

                    if (randomChoiceSplitNumber == 1)
                    {
                        randomResult = RandomSplitV2(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }
                    else
                    {
                        randomResult = RandomSplit(Random.Range(emojisMin, emojisMax), productsInLevel.Count);
                    }

                    for (int i = 0; i < productsInLevel.Count; i++)
                    {
                        productsInLevel[i].countOfProducts = randomResult[i];
                    }

                }

                break;
        }

    }
    private void GenerateLevel()
    {
        //Generatin Random Level depend on Difficulty
        //obstacle level chance
        int obstacleLevelChance = 0;
        switch (levelDifficulty)
        {
            case LevelDifficulty.Easy:
                obstacleLevelChance = 4;
                break;
            case LevelDifficulty.Mid:
                obstacleLevelChance = 8;
                break;
            case LevelDifficulty.Hard:
                obstacleLevelChance = 12;
                break;
            case LevelDifficulty.Pro:
                obstacleLevelChance = 16;
                break;
            default:
                break;
        }

        int randomChance = Random.Range(0, 100);
        bool obstacleLevel = true ? randomChance < obstacleLevelChance : randomChance >= obstacleLevelChance;
        //if obstacle level we cant put more than 3 platform with obstacle so make it random to choose between 1 and 3 platform only with obstacle
        //chance with 50% to add emojis with obstacle platform
        if (obstacleLevel)
        {
            //choose between 1 and 3 max platforms to make them with emojis

        }

        obstacleLevel = false; //testing TODO



        //Get number Of emojis in level from the list and divid them over the platform that will contain emojis
        List<int> emojisInLevelPerType = new List<int>();//save number of each type of emojis needed in level
        //  print("EmojisPerType List :::");
        for (int i = 0; i < productsInLevel.Count; i++)
        {
            int n = productsInLevel[i].countOfProducts + Random.Range(4, 7);
            emojisInLevelPerType.Add(n);
            //print("emoji type " + productsInLevel[i].productType + " has " + emojisInLevelPerType[i] + " in level");
        }

        //lets work for platforms
        //get array of index to the platforms that we will not contain obstacles
        int platformToUseCount = 0;
        for (int i = 0; i < platformControllerList.Count; i++)
        {
            if (!platformControllerList[i].containObstacles)
            {
                platformToUseCount++;
            }
        }

        int[] platformsToUseIndexArray = new int[platformToUseCount];
        //  print("platformsToUseIndex Array :::");
        for (int i = 0; i < platformControllerList.Count; i++)
        {
            if (!platformControllerList[i].containObstacles)
            {
                platformsToUseIndexArray[i] = i;
                //  print("platformsToUseIndex [" + i + "] is : " + i);
            }
        }


        // print("Distribute emojis over plaform start :::");
        //loop over list of number of emojis in level List
        for (int i = 0; i < emojisInLevelPerType.Count; i++)
        {
            //numberOfEmojiInLevelPerProductList[i] contain number of emoji type i in level should be created
            //get the number of emojis can be divided by numberOfPlatformToUse to get number of loops over platforms
            int emojisCount = emojisInLevelPerType[i];
            int loopCountOverPlatforms = Mathf.CeilToInt((float)emojisCount / (float)platformToUseCount);//10/12 = 1
                                                                                                         //   print("loopCountOverPlatforms : " + loopCountOverPlatforms + " when emojisCount is " + emojisCount);

            for (int j = 0; j < loopCountOverPlatforms; j++)
            {
                GamePreferences.ReshuffleIntArray(platformsToUseIndexArray);
                for (int k = 0; k < platformsToUseIndexArray.Length; k++)
                {
                    if (emojisCount <= 0) break;
                    //  print("create 1 new emoji in platform : " + platformsToUseIndexArray[k]);
                    platformControllerList[platformsToUseIndexArray[k]].CreateEmojiOnPlatform(ProuctsDatabase.instance.GetEmojiPrefab(productsInLevel[i].productType), platformControllerList[platformsToUseIndexArray[k]].transform);
                    emojisCount--;
                }
            }

        }
    }
    List<int> RandomSplit(int total, int count)
    {
        // print("RandomSplit for total : " + total + " and count : " + count);

        //we will split total over count but the number will be close
        //Not float result, min random = 1

        if (count > total) return null;

        List<int> randomList = new List<int>();
        int result = 0;
        int remain = total;

        for (int i = 0; i < count; i++)
        {
            int div = count - i;

            if (div <= 1)
            {
                //its the last iteration
                result = remain;
            }
            else
            {

                int n = (int)remain / (int)div;
                int d = n / 2;
                result = Random.Range(n - d, n + d);

                remain -= result;
            }
            randomList.Add(result);
        }

        return randomList;
    }

    List<int> RandomSplitV2(int total, int count)
    {
        // print("RandomSplit for total : " + total + " and count : " + count + " Version 2");

        //we will split total over count but the number will be close
        //More Randomly the numbers

        if (count > total) return null;

        List<int> randomList = new List<int>();
        int result = 0;
        int remain = total - count;

        for (int i = 0; i < count; i++)
        {

            if (i == count - 1)//last iteration
            {

                result = remain + 1;

            }
            else
            {
                result = Random.Range(0, remain);
                remain -= result;
                result++;
            }
            randomList.Add(result);
        }
        return randomList;
    }

    public void CheckWin(List<ProductInLevel> collectedProductsDuringLevel)
    {
        bool winResult = true;

        //loop and check each item if player DOES NOT get the same number of items needed player loss
        int numberOfCollectedEmojis = 0;

        if (collectedProductsDuringLevel.Count > 0)
        {
            for (int i = 0; i < collectedProductsDuringLevel.Count; i++)
            {
                numberOfCollectedEmojis += collectedProductsDuringLevel[i].countOfProducts;
                if (collectedProductsDuringLevel[i].countOfProducts != productsInLevel[i].countOfProducts)
                {
                    winResult = false;
                }
            }


        }



        if (winResult)
        {
            //player win
            UIController.instance.EndGame(true, (incomeFromLevel + incomeFromEmoji * numberOfCollectedEmojis));
            //GameManager.instance.VibrateSucess();
        }
        else
        {
            // player loss
            UIController.instance.EndGame(false, incomeFromEmoji * numberOfCollectedEmojis);
           // GameManager.instance.VibrateFailure();
        }

    }


    int GetNumberOfAllProductInLevel()
    {
        int result = 0;
        for (int i = 0; i < productsInLevel.Count; i++)
        {

            result += productsInLevel[i].countOfProducts;

        }


        //   print("all products in Level = " + result);
        return result;
    }

    int GetNumberOfAllProduct(productType productType)
    {
        int result = 0;
        for (int i = 0; i < productsInLevel.Count; i++)
        {
            if (productsInLevel[i].productType.CompareTo(productType) == 0)
            {
                result = productsInLevel[i].countOfProducts; break;
            }
        }

        return result;
    }

    bool IsProductAvailableInLevel(productType productType)
    {
        bool result = false;
        for (int i = 0; i < productsInLevel.Count; i++)
        {
            if (productsInLevel[i].productType.CompareTo(productType) == 0)
            {
                result = true; break;
            }
        }

        return result;

    }
}
