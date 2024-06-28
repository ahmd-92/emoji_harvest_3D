using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public Slider loadingBarSlider;

    private void Start()
    {
        loadingBarSlider.value = 0;
        StartLoadScene();
    }
    public void StartLoadScene() {

        StartCoroutine("LoadLevel");

    }
    IEnumerator LoadLevel()
    {

        // Asynchronous scene loading operation
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        yield return null;

        while (!operation.isDone)
        {
            // Get progress (0f to 0.9f)
            float progress = Mathf.Clamp01(operation.progress / 1f);


            // Update loading bar fill amount (consider using a reference to your loading bar UI element)
            loadingBarSlider.value = progress;

            yield return null;
        }



        // Scene loading complete
         SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneManager.sceneCountInBuildSettings - 1));
    }
}
