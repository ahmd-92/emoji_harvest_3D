using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private GameObject tutorialLevel;
    [SerializeField] private GameObject easyLevel;
    [SerializeField] private GameObject midLevel;
    [SerializeField] private GameObject hardLevel;
    [SerializeField] private GameObject expertLevel;
    [HideInInspector] public GameObject currentLevel;

    public void SpawnLevel(LevelDifficulty levelDiff)
    {

        bool tutorialLevelDone = GamePreferences.IsTutorialLevelDone();
        if (!tutorialLevelDone)
        {
            currentLevel = Instantiate(tutorialLevel, transform.position, Quaternion.identity);

        }
        else
        {
            switch (levelDiff)
            {
                case LevelDifficulty.Easy:


                    currentLevel = Instantiate(easyLevel, transform.position, Quaternion.identity);

                    break;
                case LevelDifficulty.Mid:

                    currentLevel = Instantiate(midLevel, transform.position, Quaternion.identity);


                    break;
                case LevelDifficulty.Hard:

                    currentLevel = Instantiate(hardLevel, transform.position, Quaternion.identity);


                    break;
                case LevelDifficulty.Pro:

                    currentLevel = Instantiate(expertLevel, transform.position, Quaternion.identity);


                    break;

                default:

                    currentLevel = Instantiate(easyLevel, transform.position, Quaternion.identity);


                    break;
            }

        }

        



        currentLevel.GetComponent<LevelInfo>().StartLevel(levelDiff);

    }
}
