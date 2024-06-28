using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [Header("Basics")]
    public Transform mainCamTransform;
    [Header("Camera Parents")]
    public Transform MainMenuCamParentTrans;
    public Transform runGameplayCamParentTrans;
    public Transform skinsCamParentTrans;
    public Transform mySetupGameplayCamParentTrans;
   

    [SerializeField]private List<Transform> dlightLists;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveCam(GameStatus.MainMenu);
    }


    public void MoveCam(GameStatus gameStatus) {

        Transform currentTransform = null;
        int lightID = -1;
        switch (gameStatus)
        {
            case GameStatus.MainMenu:
                currentTransform = MainMenuCamParentTrans;
                lightID = 0;
                break;
            case GameStatus.RunGame:
                currentTransform = runGameplayCamParentTrans;
                lightID = 0;
                break;
            case GameStatus.SkinsGame:
                currentTransform = skinsCamParentTrans;
                lightID = 1;
                break;
            case GameStatus.MyPcGame:
                currentTransform = mySetupGameplayCamParentTrans;
                lightID = 2;
                break;
            default:
                currentTransform = MainMenuCamParentTrans;
                lightID = 0;
                break;
        }

        foreach (Transform trans in dlightLists)
        {
            trans.gameObject.SetActive(false);
        }

        if(lightID >= 0)
        {
            dlightLists[lightID].gameObject.SetActive(true);
        }

        Moving(currentTransform);

    }

    void Moving(Transform targetTrans, float time = 0.0f)
    {
        mainCamTransform.parent = targetTrans;
        mainCamTransform.transform.DOLocalMove(Vector3.zero, time).SetEase(Ease.Linear);
        mainCamTransform.transform.DOLocalRotate(Vector3.zero, time).SetEase(Ease.Linear);
    }

    public void ShakeCamera()
    {


        mainCamTransform.DOShakePosition(0.5f, 1);
        mainCamTransform.DOShakeRotation(0.5f, 1);

    }
}
