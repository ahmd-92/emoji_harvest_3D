
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Lofelt.NiceVibrations;
using GoogleMobileAds.Api;



public enum productType { Happy, Angry, Clown, Cold, Sick, Star }
public enum LevelDifficulty { Easy, Mid, Hard, Pro }
public enum GameStatus { MainMenu, RunGame, SkinsGame, MyPcGame}
public class GameManager : MonoBehaviour
{

    [Header("General")]
    [Space(20)]
    public bool isGameOn;
    private GameStatus gameStatus;
    [Header("Vibration")]
    [Space(20)]
    public float vibrationDelay;

    public static GameManager instance;

    private bool vibrationOn = false;

    private LevelDifficulty currentLevelDiff = LevelDifficulty.Easy;

    private void Awake()
    {
        instance = this;

        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;

    }
    private void Start()
    {
        gameStatus = GameStatus.MainMenu;

    }
    public GameStatus GetGameStatus() => gameStatus;

    public void SwitchGamePlay(GameStatus status, LevelDifficulty lvlDiff = LevelDifficulty.Easy)
    {
        // Debug.Log("SwitchGamePlay called");
        gameStatus = status;
        currentLevelDiff = lvlDiff;
        //camera
        CameraManager.instance.MoveCam(status);
        switch (status)
        {
            case GameStatus.MainMenu:
                RoomGamePlayController.instance.ToggleSetupRoomComponents(false);
                break;
            case GameStatus.RunGame:
                LevelGenerator.Instance.SpawnLevel(currentLevelDiff);
                PlayerManager.Instance.InitPlayer();
                break;
            case GameStatus.SkinsGame:
                PlayerManager.Instance.InitPlayer();
                break;
            case GameStatus.MyPcGame:
                RoomGamePlayController.instance.InitSetupRoom();
                break;
            default:
#if UNITY_EDITOR
                Debug.LogError("Game Status Not available");
#endif
                break;
        }


    }
    

    public bool CheckInternet()
    {
        //return !(Application.internetReachability == NetworkReachability.NotReachable);
        return true;
    }

    public void NextLevel(float Delay)
    {
        StartCoroutine(RestartGame(Delay));
    }

    public void RestartLevel(float Delay)
    {
        StartCoroutine(RestartGame(Delay));
    }

    IEnumerator RestartGame(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySlowMotion(float SlowDuration, float SlowValue)
    {
        StartCoroutine(SlowMotion(SlowDuration, SlowValue));
    }

    IEnumerator SlowMotion(float SlowTime, float SlowValue)
    {
        Time.timeScale = 1;
        Time.timeScale = SlowValue;
        Time.fixedDeltaTime = Time.timeScale * 0.005f;
        Debug.Log("Slow Start");
        yield return new WaitForSecondsRealtime(SlowTime);
        Debug.Log("Slow End");
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        yield break;
    }

    #region Vibration

    public void VibrateLight()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
          //  HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
            // MMVibrationManager.Haptic(HapticTypes.LightImpact);
        }

        CallInvoke();
    }

    public void VibrateMedium()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
           // HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);

        }

        CallInvoke();
    }

    public void VibrateHeavy()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
            //    HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
            //MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }

        CallInvoke();
    }

    public void VibrateFailure()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
          //  HapticPatterns.PlayPreset(HapticPatterns.PresetType.Failure);

        }

        CallInvoke();
    }

    public void VibrateSucess()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
          //  HapticPatterns.PlayPreset(HapticPatterns.PresetType.Success);
        }

        CallInvoke();
    }

    public void VibrateWarning()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
            //   HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
            //MMVibrationManager.Haptic(HapticTypes.Warning);
        }
        CallInvoke();

    }

    public void VibrateRigid()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
            //  HapticPatterns.PlayPreset(HapticPatterns.PresetType.RigidImpact);
            //MMVibrationManager.Haptic(HapticTypes.Warning);
        }
        CallInvoke();
    }

    public void VibrateSelection()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
            //  HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);
            //MMVibrationManager.Haptic(HapticTypes.Warning);
        }
        CallInvoke();

    }

    public void VibrateSoftImpact()
    {
        if (GamePreferences.IsVibrationOn() && !vibrationOn)
        {
            vibrationOn = true;
            //   HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
            //MMVibrationManager.Haptic(HapticTypes.Warning);
        }
        CallInvoke();

    }
    #endregion

    void CallInvoke()
    {
        if (!IsInvoking("AllowVibration"))
        {

            Invoke("AllowVibration", vibrationDelay);
        }
    }

    void AllowVibration()
    {
        vibrationOn = false;
    }

}
