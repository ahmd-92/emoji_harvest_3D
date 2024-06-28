using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject MainMenu;

    [Header("Menu")]
    [SerializeField] private Slider loadingSlider;


    // Start is called before the first frame update
    void Start()
    {
        LoadAsync();
    }


    void LoadAsync() 
    {

        StartCoroutine("LoadGameScene");
    
    }

    IEnumerator LoadGameScene() 
    {

        AsyncOperation loadGameSceneOperation = SceneManager.LoadSceneAsync(1);

        while(!loadGameSceneOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadGameSceneOperation.progress / 0.9f);

            loadingSlider.value = progressValue;    

            yield return null;  
        }
    }
}
