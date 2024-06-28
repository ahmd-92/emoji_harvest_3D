using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePreferences : MonoBehaviour
{
    // Start is called before the first frame update
    public static string level = "level";
    public static string levelRetry = "retrylevel";
    public static string vibration = "vibration";
    public static string sound = "sound";
    public static string keys = "keys";
    public static string randomPrices = "randomPrices";
    public static string coins = "coins";

    public static string sfxVolume = "sfxVolume";
    public static string musicVolume = "musicVolume";

    public static string power = "power";
    public static string offlineEarnings = "offlineEarnings";
    public static string lastLoginDate = "lastlogindate";
    public static string offlineEarningDate = "offlinedate";
    public static string firstTimeEntry = "firsttimeentry";

    public static int GetGameCoins()
    {
        return PlayerPrefs.GetInt(coins, 0);
    }

    public static void AddGameCoins(int Coins)
    {
        // print("AddGameCoins : " + Coins);

        PlayerPrefs.SetInt(coins, Coins + GetGameCoins());
        //update UI:
        UIController.instance.UpdateCoinText();
    }

    public static void SubtractGameCoins(int Coins)
    {

        int _coins = GetGameCoins() - Coins;
        if (_coins < 0) _coins = 0; //avoid negative
        PlayerPrefs.SetInt(coins, _coins);

        //update ui:
        UIController.instance.UpdateCoinText();
    }


    public static int GetLevel()
    {
        return PlayerPrefs.GetInt(level, 0);
    }
    public static void SetLevel(int Level)
    {
        PlayerPrefs.SetInt(level, Level);
    }


    public static float GetSFxVolume()
    {
        return PlayerPrefs.GetFloat(sfxVolume, 1);
    }
    public static void SetSFxVolume(float level)
    {
        PlayerPrefs.SetFloat(sfxVolume, level);
    }


    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(musicVolume, 1);
    }
    public static void SetMusicVolume(float level)
    {
        PlayerPrefs.SetFloat(musicVolume, level);
    }




    public static bool IsVibrationOn()
    {
        return PlayerPrefs.GetInt(vibration, 1) == 1;
    }

    public static void SetVibration(bool Vibration)
    {
        int temp = Vibration ? 1 : 0;
        PlayerPrefs.SetInt(vibration, temp);
    }

    public static bool IsSoundOn()
    {
        return PlayerPrefs.GetInt(sound, 1) == 1;

    }

    public static void SetSounds(bool Sound)
    {
        int temp = Sound ? 1 : 0;
        PlayerPrefs.SetInt(sound, temp);
    }
    public static int GetLevelRetry()
    {
        return PlayerPrefs.GetInt(levelRetry, 0);
    }

    public static void SetLevelRetry(int Level)
    {
        PlayerPrefs.SetInt(levelRetry, Level);
    }


    public static string[] ReshuffleStringArray(string[] texts)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }

        return texts;
    }

    public static int[] ReshuffleIntArray(int[] intArray)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < intArray.Length; t++)
        {
            int tmp = intArray[t];
            int r = Random.Range(t, intArray.Length);
            intArray[t] = intArray[r];
            intArray[r] = tmp;
        }

        return intArray;
    }





    public static Object[] ReshuffleObjectArray(Object[] objs)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < objs.Length; t++)
        {
            Object tmp = objs[t];
            int r = Random.Range(t, objs.Length);
            objs[t] = objs[r];
            objs[r] = tmp;
        }

        return objs;
    }



    public static string FormatNumberText(int Value)
    {
        string formatedValue = "";
        if (Value < 1000)
        {
            formatedValue = Value.ToString();
        }
        else if (Value >= 1000 && Value < 1000000)
        {
            float dividedValue = (float)Value / 1000.0f;
            formatedValue = string.Format("{0:0.#}", dividedValue) + "K";
        }
        else if (Value >= 1000000 && Value < 1000000000)
        {
            float dividedValue = (float)Value / 1000000.0f;
            formatedValue = string.Format("{0:0.#}", dividedValue) + "M";
        }
        else// > 1000000000
        {
            float dividedValue = (float)Value / 1000000000.0f;
            formatedValue = string.Format("{0:0.#}", dividedValue) + "B";
        }
        return formatedValue;
    }
    public static string FormatNumberText(float Value)
    {
        string formatedValue = "";
        if (Value < 1000)
        {
            formatedValue = Value.ToString();
        }
        else if (Value >= 1000 && Value < 1000000)
        {
            float dividedValue = (float)Value / 1000.0f;
            formatedValue = string.Format("{0:0.##}", dividedValue) + "K";
        }
        else if (Value >= 1000000 && Value < 1000000000)
        {
            float dividedValue = (float)Value / 1000000.0f;
            formatedValue = string.Format("{0:0.##}", dividedValue) + "M";
        }
        else// > 1000000000
        {
            float dividedValue = (float)Value / 1000000000.0f;
            formatedValue = string.Format("{0:0.##}", dividedValue) + "B";
        }
        return formatedValue;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (min < 0 && max > 0 && (angle > max || angle < min))
        {
            angle -= 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }
        else if (min > 0 && (angle > max || angle < min))
        {
            angle += 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }

        if (angle < min) return min;
        else if (angle > max) return max;
        else return angle;
    }

    public static int GetPower()
    {
        return PlayerPrefs.GetInt(power, 5);
    }

    public static void SetPower(int value)
    {
        PlayerPrefs.SetInt(power, value);
    }

    public static int GetOfflineEarnings()
    {
        return PlayerPrefs.GetInt(offlineEarnings, 1);
    }

    public static void SetOfflineEarnings(int value)
    {
        PlayerPrefs.SetInt(offlineEarnings, value);
    }

    public static void SetLastLoginDate(System.DateTime CurrentTime)
    {
        PlayerPrefs.SetString(lastLoginDate, CurrentTime.ToString());
    }

    public static System.DateTime GetLastLoginDate()
    {
        return System.Convert.ToDateTime(PlayerPrefs.GetString(lastLoginDate, System.DateTime.UtcNow.ToString()));
    }

    public static void SetOfflineDate(System.DateTime CurrentTime)
    {
        PlayerPrefs.SetString(offlineEarningDate, CurrentTime.ToString());
    }

    public static System.DateTime GetOfflineDate()
    {
        return System.Convert.ToDateTime(PlayerPrefs.GetString(offlineEarningDate, System.DateTime.UtcNow.ToString()));
    }

    public static void SetFirstTimeEntry(int Value)
    {
        PlayerPrefs.SetInt(firstTimeEntry, Value);
    }

    public static int GetFirstTimeEntry()
    {
        return PlayerPrefs.GetInt(firstTimeEntry, 0);
    }

    #region Level Saves
    private static readonly int numberOfLevelsToUnlockNewDiff = 4;
    private  static readonly string tutorialLevelDone = "tutorialLevelDone";
    public static bool IsTutorialLevelDone()
    {
        bool result = false;
        if (PlayerPrefs.GetInt(tutorialLevelDone, 0) > 0)
        {
            result = true;
        }
        return result;
    }

    public static void TutorialLevelDone()
    {
        PlayerPrefs.SetInt(tutorialLevelDone, 1);
    }

    public static string currentLevelDifficulty = "currentLevelDifficulty";

    public static int GetCurrentLevelDifficulty()
    {
        return PlayerPrefs.GetInt(currentLevelDifficulty, 0);
    }

    public static void LevelUpDifficulty()
    {
        //when player successfully win 10 levels of current level of difficulty
        //but cannot be more than 3
        if (GetCurrentLevelDifficulty() < 3)
        {
            PlayerPrefs.SetInt(currentLevelDifficulty, GetCurrentLevelDifficulty() + 1);
        }

    }

    //counter till new level
    public static string currrentLevelCounterToLevelUp = "currrentLevelCounterToLevelUp";

    public static int GetLevelCountToLevelUp()
    {
        return PlayerPrefs.GetInt(currrentLevelCounterToLevelUp, numberOfLevelsToUnlockNewDiff);
    }
    public static void SubLevelCountToLevelUp()
    {

        PlayerPrefs.SetInt(currrentLevelCounterToLevelUp, GetLevelCountToLevelUp() - 1);

        //under expert level < 3
        if (GetCurrentLevelDifficulty() < 3)
        {
            if (GetLevelCountToLevelUp() == 0)
            {
                print("reset count to level up");
                //level Up:
                LevelUpDifficulty();
                ResetLevelCountToLevelUp();

            }
        }
        else
        {
            if (GetLevelCountToLevelUp() <= 0)
            {
                PlayerPrefs.SetInt(currrentLevelCounterToLevelUp, -1);
            }

        }

    }
    public static void ResetLevelCountToLevelUp()
    {
        //when player successfully win 10 levels of current level of difficulty
        PlayerPrefs.SetInt(currrentLevelCounterToLevelUp, numberOfLevelsToUnlockNewDiff);
    }
    #endregion
}

